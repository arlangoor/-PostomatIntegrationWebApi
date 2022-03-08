using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PostomatIntegration.DAL.Repositories;
using PostomatIntegration.BL.Models;
using System.Linq;
using AttributeExtentions;
using PostomatIntegration.DAL.Entities;
using PostomatIntegration.BL.Models.WebModels;
using PostomatIntegration.BL.CustomExceptions;

namespace PostomatIntegration.BL.Servicies
{
	[RegisterService]
	public class OrderService
	{
		#region Services and Repositories
		private readonly OrderRepository _orderRepository;
		private readonly OrderContentRepository _orderContentRepository;
		private readonly PostomatService _postomatService;
		private readonly ValidationService _validationService;
		#endregion Services
		#region .ctor
		public OrderService(
			OrderRepository orderRepository
			, OrderContentRepository orderContentRepository
			, PostomatService postomatService
			, ValidationService validationService
			)
		{
			_orderRepository = orderRepository;
			_orderContentRepository = orderContentRepository;
			_postomatService = postomatService;
			_validationService = validationService;
		}
		#endregion .ctor
		#region private methods
		private async Task<Order> CheckExistingOrder(int orderNumber)
		{
			var existingOrder = await GetExistingOrder(orderNumber);
			if (existingOrder == null)
				throw new ObjectNotFoundException();

			return existingOrder;
		}
		private async Task<Order> GetExistingOrder(int orderNumber)
		=> (await _orderRepository.ExecuteQueryAsync(Orders.Where(x => x.Number == orderNumber)))
			.FirstOrDefault();
		
		private Order ChangeOrder(OrderRequest request, Order existingOrder)
		{
			existingOrder.CustomerFIO = request.CustomerFIO;
			existingOrder.CustomerPhone = request.CustomerNumber;
			existingOrder.Price = request.Price.Value;
			return existingOrder;
		}
		private Order ConstructOrder(OrderRequest request, Postomat postomat)
			=> new Order()
			{
				PostomatId = postomat.Id,
				Number = request.OrderNumber,
				Status = request.OrderStatus,
				CustomerFIO = request.CustomerFIO,
				CustomerPhone = request.CustomerNumber,
				Price = request.Price.Value
			};
		private async Task<Postomat> GetPostomat(string postomatNumber)
		{
			var postomat = (Postomat)(await _postomatService.GetPostOfficeInfoAsync(postomatNumber));

			if (postomat == null)
				throw new ObjectNotFoundException();

			if (postomat.Status == false)
				throw new ForbiddenException();

			return postomat;
		}
		#endregion private methods

		#region public methods
		public async Task CancelOrderAsync(int orderNumber)
		{
			var order = await CheckExistingOrder(orderNumber);

			order.Status = DAL.Enums.OrderStatusEnum.Canceled;

			await UpdateOrderAsync((Order)order);

		}
		public async Task<OrderRequest> GetOrderInfo(int orderNumber)
		{
			var orders = await _orderRepository.ExecuteQueryAsync(Orders.Where(x => x.Number == orderNumber));

			var content =await  _orderContentRepository.ExecuteQueryAsync(Contents.Where(x => x.Order.Number == orderNumber));

			var order = orders
				.FirstOrDefault();

			if (order == null)
				throw new ObjectNotFoundException();

			if (order.Postomat == null)
				order.Postomat = await _postomatService.GetPostomatAsync(order.PostomatId.Value);

			List<string> contentList = content.Any()? content.Select(x => x.Content).ToList(): new List<string>();

			OrderRequest response = new OrderRequest()
			{
				Content = contentList,
				OrderNumber = order.Number,
				CustomerNumber = order.CustomerPhone,
				OrderStatus = order.Status,
				CustomerFIO = order.CustomerFIO,
				Postomat  = order.Postomat.Number,
				Price = order.Price
			};

			return response;
		}
		public async Task UpdateOrderAsync(OrderRequest request)
		{
			var existingOrder = await CheckExistingOrder(request.OrderNumber);

			existingOrder.Postomat = await _postomatService.GetPostomatAsync(existingOrder.PostomatId.Value);

			_validationService.ChangeValidationRules(existingOrder, request);

			var existing_content = await _orderContentRepository.ExecuteQueryAsync(Contents.Where(x => x.OrderId == existingOrder.Id));

			using (var transaction = await _orderRepository.BeginTransactionAsync())
			{
				await UpdateOrderAsync(ChangeOrder(request, existingOrder));

				if (existing_content.Any())
				{
					foreach (var contentPart in existing_content)
						await _orderContentRepository.DeleteAsync(contentPart);
				}
				if (request.Content.Any())
				{
					foreach (var contentPart in request.Content)
						await _orderContentRepository.InsertAsync(new OrderContent() { 
							OrderId = existingOrder.Id,
							Content = contentPart
						});
				}

				await transaction.CommitAsync();
			}
		}
		public async Task CreateOrder(OrderRequest request)
		{
			if ((await GetExistingOrder(request.OrderNumber)) != null)
				throw new ValidationException();

			_validationService.CreateValidationRules(request);

			var order = ConstructOrder(request, await GetPostomat(request.Postomat));

			using (var tran = await _orderRepository.BeginTransactionAsync())
			{
				await InsertOrderAsync(order);

				foreach (var contentPart in request.Content)
				{
					await _orderContentRepository.InsertAsync(new OrderContent()
					{
						OrderId = order.Id,
						Content = contentPart
					});
				}

				await tran.CommitAsync();
			}
		}
		#endregion public methods
		#region virtual
		protected virtual async Task<bool> InsertOrderAsync(Order order)
		{
			await _orderRepository.InsertAsync(order);
			return true;
		}
		protected virtual async Task<bool> UpdateOrderAsync(Order order)
		{
			await _orderRepository.UpdateAsync(order);
			return true;
		}
		protected virtual IQueryable<Order> Orders => _orderRepository.Query;
		protected virtual IQueryable<OrderContent> Contents => _orderContentRepository.Query;
		#endregion virtual
	}
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PostomatIntegration.BL.Models;
using PostomatIntegration.BL.Servicies;
using System.ComponentModel.DataAnnotations;
using PostomatIntegration.BL.Models.WebModels;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;

namespace PostomatIntegration.WebApi.Controllers
{
	/// <summary>
	/// Контроллер для работы с заказами
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly OrderService _orderService;
		public OrderController(
			OrderService orderService
			)
		{
			_orderService = orderService;
		}
		/// <summary>
		/// Создание заказа
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("create-order")]
		public async Task<IActionResult> CreateOrderaAsync([Required][FromBody] OrderRequest request)
		{
			
			try 
			{
				await _orderService.CreateOrder(request);
				return new OkResult();
			}
			catch (Exception ex)
			{
				return WebResponseFactory.GetResponse(ex);
			}
		}
		/// <summary>
		/// Обновление заказа
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPut]
		[Route("change-order")]
		public async Task<IActionResult> ChangeOrderAsync([Required][FromBody] OrderRequest request)
		{
			try
			{
				await _orderService.UpdateOrderAsync(request);
				return new OkResult();
			}
			catch (Exception ex)
			{
				return WebResponseFactory.GetResponse(ex);
			}
		}
		/// <summary>
		/// Получение информации о заказе
		/// </summary>
		/// <param name="orderNumber"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("order-info")]
		public async Task<IActionResult> GetOrderInfo([Required][FromQuery] int orderNumber)
		{
			try 
			{
				var result = await _orderService.GetOrderInfo(orderNumber);
				return new ObjectResult(result);
			}
			catch (Exception ex)
			{
				return WebResponseFactory.GetResponse(ex);
			}
		}
		/// <summary>
		/// Отмена заказа
		/// </summary>
		/// <param name="orderNumber"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("cancel-order")]
		public async Task<IActionResult> CancelOrder([Required][FromQuery] int orderNumber)
		{
			try
			{
				await _orderService.CancelOrderAsync(orderNumber);
				return new OkResult();
			}
			catch (Exception ex)
			{
				return WebResponseFactory.GetResponse(ex);
			}
		}
		

	}
}

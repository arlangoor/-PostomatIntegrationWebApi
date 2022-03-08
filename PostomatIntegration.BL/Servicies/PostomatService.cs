using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PostomatIntegration.BL.Models;
using PostomatIntegration.DAL.Interfaces;
using PostomatIntegration.DAL.Repositories;
using System.Linq;
using AttributeExtentions;
using PostomatIntegration.DAL.Entities;
using PostomatIntegration.BL.CustomExceptions;

namespace PostomatIntegration.BL.Servicies
{
	[RegisterService]
	public class PostomatService
	{
		private readonly PostomatRepository _postomatRepository;
		private readonly ValidationService _validationService;
		public PostomatService(PostomatRepository postomatRepository
			, ValidationService validationService
			)
		{
			_postomatRepository = postomatRepository;
			_validationService = validationService;
		}

		public async Task<IPostomat> GetPostOfficeInfoAsync(string postOfficeNumber)
		{
			if (_validationService.PostomatFormatRule(postOfficeNumber) == false)
				throw new ValidationException();

			var result = await _postomatRepository.ExecuteQueryAsync(Postomats.Where(x => x.Number == postOfficeNumber));

			if (result.Any() == false)
				throw new ObjectNotFoundException();

			return result.First();
		}

		public async Task<List<IPostomat>> GetPostomatsAsync(GetOpenedPostOfficeListRequest request)
		{
			var query = Postomats;

			query = query.OrderBy(x => x.Number);

			if (request.TargetPage.HasValue && request.NumberPerPage.HasValue)
				query = query.Skip((request.TargetPage.Value - 1) * request.NumberPerPage.Value).Take(request.NumberPerPage.Value);

			var postomats = await _postomatRepository.ExecuteQueryAsync(query);

			return postomats.Select(x => (IPostomat)x).ToList();
		}

		public async Task<Postomat> GetPostomatAsync(int Id)
			=> (await _postomatRepository.ExecuteQueryAsync(Postomats.Where(x => x.Id == Id))).FirstOrDefault();

		protected virtual IQueryable<Postomat> Postomats => _postomatRepository.Query;
	}
}

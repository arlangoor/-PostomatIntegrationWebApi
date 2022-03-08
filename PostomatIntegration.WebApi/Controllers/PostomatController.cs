using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PostomatIntegration.BL.Models;
using PostomatIntegration.BL;
using PostomatIntegration.DAL.Interfaces;
using System.ComponentModel.DataAnnotations;
using PostomatIntegration.BL.Servicies;

namespace PostomatIntegration.WebApi.Controllers
{
	/// <summary>
	/// Контроллер получения информации о постоматах
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class PostomatController : ControllerBase
	{
		private readonly PostomatService _postomatService;
		public PostomatController(PostomatService postomatService)
		{
			_postomatService = postomatService;
		}
		/// <summary>
		/// Получение списка рабочих постаматов, отсортированных по номеру в алфавитном порядке
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		[Route("get-opened-post-office-list")]
		public async Task<IActionResult> GetOpenedPostOfficeListAsync([FromBody] GetOpenedPostOfficeListRequest request)
		{
			
			try 
			{
				var result = await _postomatService.GetPostomatsAsync(request);
				return new ObjectResult(result);
			}
			catch (Exception ex)
			{
				return WebResponseFactory.GetResponse(ex);
			}
		}
		/// <summary>
		/// Получение информации о постамате
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("get-post-office-info")]
		public async Task<IActionResult> GetPostOfficeInfoAsync([Required][FromQuery] string postomatNumber)
		{
			try
			{
				var result = await _postomatService.GetPostOfficeInfoAsync(postomatNumber);
				return new ObjectResult(result);
			}
			catch (Exception ex)
			{
				return WebResponseFactory.GetResponse(ex);
			} 
		}
	}
}

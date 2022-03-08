using System;
using System.Collections.Generic;
using System.Text;
using AttributeExtentions;
using Microsoft.AspNetCore.Mvc;
using PostomatIntegration.BL.CustomExceptions;

namespace PostomatIntegration.BL.Servicies
{
	public static class WebResponseFactory
	{
		public static IActionResult GetResponse(Exception exception)
		{
			if (exception as ValidationException != null)
				return new BadRequestResult();

			if (exception as ForbiddenException != null)
				return new StatusCodeResult(403);

			if (exception as ObjectNotFoundException != null)
				return new NotFoundResult();

			throw exception;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace PostomatIntegration.BL.Models
{
	public class BasePaginateRequest
	{
		public int? TargetPage { get; set; }
		public int? NumberPerPage { get; set; } = 100;
	}
}

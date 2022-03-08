using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using PostomatIntegration.DAL.Enums;

namespace PostomatIntegration.BL.Models.WebModels
{
	public class OrderRequest
	{
		[Required]
		public int OrderNumber { get; set; }
		public OrderStatusEnum OrderStatus { get; set; }
		public List<string> Content { get; set; } = new List<string>();
		public decimal? Price { get; set; } = decimal.Zero;
		[Required]
		public string Postomat { get; set; }
		[Required]
		public string CustomerNumber { get; set; }
		[Required]
		public string CustomerFIO { get; set; }
	}
}

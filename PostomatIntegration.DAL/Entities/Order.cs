using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using PostomatIntegration.DAL.Enums;

namespace PostomatIntegration.DAL.Entities
{
	[Table("Orders")]
	public class Order
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
		public int Number { get; set; }
		public OrderStatusEnum Status { get; set; }
		public decimal Price { get; set; }
		[ForeignKey("Postomat")]
		public int? PostomatId { get; set; }
		public virtual Postomat Postomat { get; set; }
		public string CustomerFIO { get; set; }
		public string CustomerPhone { get; set; }
	}
}

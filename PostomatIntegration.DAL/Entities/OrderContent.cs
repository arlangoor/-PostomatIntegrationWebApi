using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PostomatIntegration.DAL.Entities
{
	 
	[Table("OrderContents")]
	public	class OrderContent
	{ 
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[ForeignKey("Order")]
		public int OrderId { get; set; }
		public virtual Order Order { get; set; }
		public string Content { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using PostomatIntegration.DAL.Contexts;
using PostomatIntegration.DAL.Entities;
using AttributeExtentions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace PostomatIntegration.DAL.Repositories
{
	[RegisterService]
	public class OrderRepository: BaseRepository<Order>
	{
		public OrderRepository(PostomatIntegrationDBContext postomatIntegrationDBContext) : base(postomatIntegrationDBContext.Orders) {
			
		}
	}
}

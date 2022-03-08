using System;
using System.Collections.Generic;
using System.Text;
using PostomatIntegration.DAL.Entities;
using PostomatIntegration.DAL.Contexts;
using AttributeExtentions;

namespace PostomatIntegration.DAL.Repositories
{
	[RegisterService]
	public class OrderContentRepository:BaseRepository<OrderContent>
	{
		public OrderContentRepository(PostomatIntegrationDBContext postomatIntegrationDBContext) : base(postomatIntegrationDBContext.OrderContents) { }
	}
}

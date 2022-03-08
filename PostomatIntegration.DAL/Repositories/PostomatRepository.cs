using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PostomatIntegration.DAL.Entities;
using PostomatIntegration.DAL.Contexts;
using System.Threading.Tasks;
using System.Linq;
using AttributeExtentions;

namespace PostomatIntegration.DAL.Repositories
{
	[RegisterService]
	public class PostomatRepository: BaseRepository<Postomat>
	{
		public PostomatRepository(PostomatIntegrationDBContext postomatIntegrationDBContext) : base(postomatIntegrationDBContext.Postomats) { }
	}
}

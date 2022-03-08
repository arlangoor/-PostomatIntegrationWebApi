using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PostomatIntegration.DAL.Entities;

namespace PostomatIntegration.DAL.Contexts
{
	public class PostomatIntegrationDBContext : DbContext
	{
		public PostomatIntegrationDBContext(DbContextOptions<PostomatIntegrationDBContext> options) : base(options) { }
		public DbSet<Postomat> Postomats { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderContent> OrderContents { get; set; }
	}
}

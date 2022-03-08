using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace PostomatIntegration.DAL.Contexts
{
    public class BrokerTaskDesignTimeFactory : IDesignTimeDbContextFactory<PostomatIntegrationDBContext>
    {
		public PostomatIntegrationDBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();

            var builder = new DbContextOptionsBuilder<PostomatIntegrationDBContext>();

            var connectionString = configuration.GetConnectionString("PostomatIntegrationDB");

            builder.UseSqlServer(connectionString);

            return new PostomatIntegrationDBContext(builder.Options);
        }
    }
}

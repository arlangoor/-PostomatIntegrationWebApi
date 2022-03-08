using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using PostomatIntegration.DAL.Contexts;

namespace PostomatIntegration.DAL
{
	public static class ServiceCollectionExtensions
	{
		public static void RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
		{
			services.RegisterMsSqlDbContext<PostomatIntegrationDBContext>(() => configuration.GetConnectionString("PostomatIntegrationDB"));
		}
        public static void RegisterMsSqlDbContext<TContextImplementation>(
           this IServiceCollection services,
           Func<string> GetConnectionString,
           ILoggerFactory loggerFactory = null)
               where TContextImplementation : DbContext
        {
            services.AddDbContext<TContextImplementation>(options =>
            {
                if (loggerFactory != null) { options.UseLoggerFactory(loggerFactory); }
                options.UseSqlServer(GetConnectionString());
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddScoped<TContextImplementation>();
        }
    }
}

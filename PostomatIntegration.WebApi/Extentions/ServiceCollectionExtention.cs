using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttributeExtentions;

namespace PostomatIntegration.WebApi.Extentions
{
	public static class ServiceCollectionExtention
	{
        public static void RegisertAssemblyClassesByAttributes<TAssemblyMember>(this IServiceCollection services)
        {
            var assembly = typeof(TAssemblyMember).Assembly;

            foreach (var assemblyDefinedType in assembly.GetTypes().Where(t => t.IsClass))
            {
                var attributeObj = assemblyDefinedType
                    .GetCustomAttributes(typeof(RegisterServiceAttribute), false)
                    .FirstOrDefault();

                var attribute = attributeObj as RegisterServiceAttribute;
                if (attribute == null)
                    continue;

                services.Add(new ServiceDescriptor(assemblyDefinedType, assemblyDefinedType, ServiceLifetime.Scoped));
            }
        }
    }
}

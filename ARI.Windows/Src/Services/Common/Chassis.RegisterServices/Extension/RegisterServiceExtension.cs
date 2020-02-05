using System.Linq;
using System.Reflection;
using Chassis.RegisterServices.Utility;
using Microsoft.Extensions.DependencyInjection;

namespace Chassis.RegisterServices.Extension
{
    /// <summary>
    /// Contains extension method for Service registration
    /// </summary>
    public static class RegisterServiceExtension
    {
        /// <summary>
        /// Extension method used to register service with or without assemblies
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddRegisterService(this IServiceCollection services, params Assembly[] assemblies)
        {
            assemblies = assemblies.Append(Assembly.GetCallingAssembly())
                                    .Distinct().ToArray();

            var assemblyIterator = new AssemblyIterator();
            foreach (var assembly in assemblies)
            {
                assemblyIterator.FindServiceRegistrar(services, assembly);
            }

            return services;
        }
    }
}

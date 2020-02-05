using Chassis.EmbeddedResource.Interface;
using Chassis.EmbeddedResource.Utility;
using Microsoft.Extensions.DependencyInjection;

namespace Chassis.EmbeddedResource.ExtensionMethods
{
    public static class EmbeddedResourcesServiceExtensions
    {
        /// <summary>
        /// Adds OneRpp EmbeddedResource services to the specified Microsoft.Extensions.DependencyInjection.IServiceCollection.
        /// </summary>
        /// <param name="services">The Microsoft.Extensions.DependencyInjection.IServiceCollection to add services to.</param>
        /// <param name="assemblyName">To pass external assembly path</param>
        public static void AddEmbeddedSqlResource(this IServiceCollection services, string[] assemblyName)
        {
            var loadResourceAssembly = new LoadResourceAssembly();
            loadResourceAssembly.LoadResourcedAssembly(assemblyName);
            services.AddSingleton(loadResourceAssembly);
            services.AddSingleton<IResourceReader, ResourceReader>();
        }
    }
}

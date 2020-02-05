using System;
using System.Linq;
using System.Reflection;
using Chassis.Dapper.Interfaces;
using Chassis.Dapper.Interfaces.Query;
using Chassis.Dapper.Services;
using Chassis.Dapper.Utility;
using Chassis.EmbeddedResource.ExtensionMethods;
using Chassis.EmbeddedResource.Interface;
using Chassis.EmbeddedResource.Utility;
using Microsoft.Extensions.DependencyInjection;

namespace Chassis.Dapper.Extension
{
    /// <summary>
    /// Extension class used to add Dapper 
    /// </summary>
    public static class DapperServiceExtension
    {
        /// <summary>
        /// Extension method used to add Dapper Services
        /// </summary>
        /// <param name="services">service collection object</param>
        /// <param name="options">Action to pass connection value</param>
        /// <param name="embeddedSqlAssemblyName">Assemblies to scan for embedded resource</param>
        /// <returns>service collection object</returns>
        public static IServiceCollection AddDapper(this IServiceCollection services, Action<ConnectionString> options, params Assembly[] embeddedSqlAssemblyName)
        {
            services.AddSingleton(x =>
            {
                ConnectionString connectionString = new ConnectionString();
                options(connectionString);
                return connectionString;
            });

            services.AddEmbeddedSqlResource(embeddedSqlAssemblyName.Select(x => x.GetName().Name).ToArray());
            services.AddSingleton<IResourceReader, ResourceReader>();
            services.AddSingleton<IRazor, Razor>();
            services.AddSingleton<ISqlQuery, SqlQuery>();
            services.AddTransient<IConnection, Connection>();
            services.AddTransient<IQueryExecuter, QueryExecuter>();
            services.AddTransient<IDapperWrapper, DapperWrapper>();
            services.AddTransient<IQueryDispatcher, QueryDispatcher>();
            services.AddSingleton<IMapperConfigurationFactory, MapperConfigurationFactory>();

            //Added
            services.AddQuery();

            return services;
        }
    }
}

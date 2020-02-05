using System;
using Chassis.Repository.EntityFramework.Factory;
using Chassis.Repository.EntityFramework.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Chassis.Repository.EntityFramework.Extension
{
    /// <summary>
    /// Contain extension method for entity framework service register
    /// </summary>
    public static class EntityFramworkServiceExtension
    {
        /// <summary>
        /// Extension method used to add entity framework context
        /// </summary>
        /// <param name="service">IServiceCollection</param>
        /// <param name="optionsBuilder">Action<DbContextOptionsBuilder<GenericDbContext>></param>
        /// <param name="modelBuilder">Action<ModelBuilder></param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddEfDbContext<T>(this IServiceCollection service,
            Action<DbContextOptionsBuilder<T>> optionsBuilder,
            Action<ModelBuilder> modelBuilder, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped) where T : GenericDbContext
        {
            service.AddSingleton<EfDbContextFactory>();

            service.Add(new ServiceDescriptor(
                typeof(DbContextOptionsBuilder<T>),
                _ =>
                {
                    var dbContextOptionsBuilder = new DbContextOptionsBuilder<T>();
                    optionsBuilder(dbContextOptionsBuilder);
                    return dbContextOptionsBuilder;
                },
                ServiceLifetime.Singleton));

            service.Add(new ServiceDescriptor(typeof(IContext),
                x =>
                {
                    var efDbContextFactory = x.GetService<EfDbContextFactory>();
                    var dbContext = efDbContextFactory.CreateEfContext<T>(modelBuilder);
                    return new EntityFrameworkContext(dbContext);
                }, serviceLifetime));

            return service;
        }

        /// <summary>
        /// Extension method used to add entity framework context
        /// </summary>
        /// <param name="service">IServiceCollection</param>
        /// <param name="optionsBuilder">Action<DbContextOptionsBuilder<GenericDbContext>></param>
        /// <param name="modelBuilder"> Action<ModelBuilder></param>
        /// <param name="serviceLifetime">ServiceLifetime</param>
        /// <returns></returns>
        public static IServiceCollection AddEfContext(this IServiceCollection service,
            Action<DbContextOptionsBuilder<GenericDbContext>> optionsBuilder, Action<ModelBuilder> modelBuilder,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            service.AddEfDbContext<GenericDbContext>(optionsBuilder, modelBuilder, serviceLifetime);

            return service;
        }

    }
}

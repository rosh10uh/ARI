using FluentNHibernate.Cfg;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using Chassis.Repository.NHibernate.Factory;
using System;

namespace Chassis.Repository.NHibernate
{
    /// <summary>
    /// Contains extension method for NHibernate Service
    /// </summary>
    public static class NHibernateServiceCollectionExtensions
    {
        /// <summary>
        /// Extension method used to Add NHibernate Context
        /// </summary>
        /// <param name="services">Service object</param>
        /// <param name="action">FluentConfiguration object</param>
        /// <returns>It returns services</returns>
        public static IServiceCollection AddNHibernate(this IServiceCollection services,
            Action<FluentConfiguration> action, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            services.AddSingleton<NHibernateSessionFactory>();
            services.Add(new ServiceDescriptor(typeof(ISessionFactory), x =>
            {
                FluentConfiguration configuration = Fluently.Configure();
                action(configuration);
                return configuration.BuildSessionFactory();
            }, ServiceLifetime.Singleton));

            services.Add(new ServiceDescriptor(typeof(IContext), x =>
            {
                var nhibernateSessionFactory = x.GetService<NHibernateSessionFactory>();
                return nhibernateSessionFactory.CreateNHibernateContext();
            }, serviceLifetime));

            return services;
        }
    }
}

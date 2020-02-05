using System;
using Chassis.RegisterServices.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Chassis.RegisterServices.Attributes
{
    /// <summary>
    /// Used as annotations to register services
    /// </summary>
    public class ServiceAttribute : Attribute
    {
        /// <summary>
        /// Service lifetime
        /// </summary>
        public ServiceLifetime ServiceLifetime { get; }

        /// <summary>
        /// Initializes the Service attribute
        /// </summary>
        /// <param name="lifeTime"></param>
        public ServiceAttribute(ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        {
            ServiceLifetime = serviceLifetime;
        }

        /// <summary>
        /// Used to register each type based on its lifetime
        /// </summary>
        /// <param name="services"></param>
        /// <param name="type"></param>
        public void Register(IServiceCollection services, Type type)
        {
            BaseRegistration baseRegistration;
            switch (ServiceLifetime)
            {
                case ServiceLifetime.Scoped:
                    baseRegistration = new ScopeRegistration(services);
                    break;
                case ServiceLifetime.Singleton:
                    baseRegistration = new SingletonRegistration(services);
                    break;
                default:
                    baseRegistration = new TransientRegistration(services);
                    break;
            }
            baseRegistration.Register(type);
        }
    }
}

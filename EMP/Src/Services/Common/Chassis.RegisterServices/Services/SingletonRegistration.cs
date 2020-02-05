using Microsoft.Extensions.DependencyInjection;
using System;

namespace Chassis.RegisterServices.Services
{
    /// <summary>
    /// Registers service with Singleton lifetime
    /// </summary>
    public class SingletonRegistration : BaseRegistration
    {
        /// <summary>
        /// Initializaes Singleton registration
        /// </summary>
        /// <param name="services">IServiceCollection object</param>
        public SingletonRegistration(IServiceCollection services) : base(services)
        {
        }

        /// <summary>
        /// Register each type with its interface
        /// </summary>
        /// <param name="interfaceType">Interface type</param>
        /// <param name="implementation">Implementation type</param>
        public override void WithServiceType(Type interfaceType, Type implementation)
        {
            Services.AddSingleton(interfaceType, implementation);
        }

        /// <summary>
        /// Register types without interface
        /// </summary>
        /// <param name="implementation">Implementation type</param>
        public override void WithoutServicetype(Type implementation)
        {
            Services.AddSingleton(implementation);
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using System;

namespace Chassis.RegisterServices.Services
{
    /// <summary>
    /// Registers service with Transient lifetime
    /// </summary>
    public class TransientRegistration : BaseRegistration
    {
        /// <summary>
        /// Initializaes Transient registration
        /// </summary>
        /// <param name="services">IServiceCollection object</param>
        public TransientRegistration(IServiceCollection services) : base(services)
        {
        }

        /// <summary>
        /// Register each type with its interface
        /// </summary>
        /// <param name="interfaceType">Interface type</param>
        /// <param name="implementation">Implementation type</param>
        public override void WithServiceType(Type interfaceType, Type implementation)
        {
            Services.AddTransient(interfaceType, implementation);
        }

        /// <summary>
        /// Register types without interface
        /// </summary>
        /// <param name="implementation">Implementation type</param>
        public override void WithoutServicetype(Type implementation)
        {
            Services.AddTransient(implementation);
        }
    }
}

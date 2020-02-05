using Microsoft.Extensions.DependencyInjection;
using System;

namespace Chassis.RegisterServices.Services
{
    /// <summary>
    /// Base registration class that registers each type with a lifetime
    /// </summary>
    public abstract class BaseRegistration
    {
        protected IServiceCollection Services { get; }

        /// <summary>
        /// Initializaes Base registration
        /// </summary>
        /// <param name="services">IServiceCollection object</param>
        public BaseRegistration(IServiceCollection services)
        {
            Services = services;
        }

        /// <summary>
        /// Registers each type with or without their interface
        /// </summary>
        /// <param name="type">Type of service to register</param>
        public void Register(Type type)
        {
            var interfaces = type.GetInterfaces();
            if (interfaces.Length > 0)
            {
                WithServiceType(interfaces[0], type);
            }
            else if (type.BaseType?.BaseType != null)
            {
                WithServiceType(type.BaseType, type);
            }
            else
            {
                WithoutServicetype(type);
            }
        }

        /// <summary>
        /// Register each type with its interface
        /// </summary>
        /// <param name="interfaceType">Interface type</param>
        /// <param name="implementation">Implementation type</param>
        public abstract void WithServiceType(Type interfaceType, Type implementation);

        /// <summary>
        /// Register types without interface
        /// </summary>
        /// <param name="implementation">Implementation type</param>
        public abstract void WithoutServicetype(Type implementation);
    }
}

using Microsoft.Extensions.DependencyInjection;
using Chassis.RegisterServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using Chassis.RegisterServices.Attributes;

namespace Chassis.RegisterServices.Utility
{
    /// <summary>
    /// Iterates through assembly to scan all the services
    /// </summary>
    public class AssemblyIterator : IAssemblyIterator
    {
        /// <summary>
        /// Registers the types that have service attribute
        /// </summary>
        /// <param name="serviceCollection">Service object</param>
        /// <param name="assembly">Assembly to register service</param>
        public void FindServiceRegistrar(IServiceCollection serviceCollection, Assembly assembly)
        {
            foreach (var service in GetServiceAttribute(assembly.GetTypes()))
            {
                service.Item1.Register(serviceCollection, service.Item2);
            }
        }

        /// <summary>
        /// Finds Service attribute on the types
        /// </summary>
        /// <param name="types">All types to check for attribute</param>
        /// <returns>returns the type that has attribute</returns>
        private IEnumerable<(ServiceAttribute, Type)> GetServiceAttribute(Type[] types)
        {
            foreach (var type in types)
            {
                var serviceAttribute = type.GetCustomAttribute<ServiceAttribute>();
                if (serviceAttribute != null)
                {
                    yield return (serviceAttribute, type);
                }
            }
        }
    }
}

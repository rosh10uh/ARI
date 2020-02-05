using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Chassis.RegisterServices.Interfaces
{
    /// <summary>
    /// Interface for Assembly Iterator
    /// </summary>
    internal interface IAssemblyIterator
    {
        /// <summary>
        /// Finds all Types that are annoted with ServiceAttribute
        /// </summary>
        /// <param name="serviceCollection">Service object</param>
        /// <param name="assembly">Assembly to register service</param>
        void FindServiceRegistrar(IServiceCollection serviceCollection, Assembly assembly);
    }
}

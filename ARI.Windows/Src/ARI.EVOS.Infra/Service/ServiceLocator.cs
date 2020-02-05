using System;

namespace ARI.EVOS.Infra.Service
{
    /// <summary>
    /// Use to get Service of IServiceProvider
    /// </summary>
    public static class ServiceLocator
    {
        /// <summary>
        /// Get Service from IServiceProvider
        /// </summary>
        public static IServiceProvider InstanceProvider { get; set; }

        /// <summary>
        /// Use to add IService Provider service
        /// </summary>
        /// <param name="provider">Use to set service of IServiceProvider</param>
        public static void AddProvider(IServiceProvider provider)
        {
            InstanceProvider = provider;
        }
    }
}

using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Chassis.Logger.ExtensionMethods
{
    public static class Log4NetBuilderExtensions
    {
        /// <summary>
        /// Adds Log4Net to the IServiceProvider request execution.
        /// </summary>
        /// <param name="app">The Microsoft.AspNetCore.Builder.IApplicationBuilder.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static ILoggerFactory UseLog4Net(this IServiceProvider app)
        {
            return GetLoggerFactory(app).AddLog4Net();
        }

        /// <summary>
        /// Adds Log4Net to the IServiceProvider request execution.
        /// </summary>
        /// <param name="app">The IServiceProvider.</param>
        /// <param name="log4NetConfigFile">Set log4net config file path</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static ILoggerFactory UseLog4Net(this IServiceProvider app, string log4NetConfigFile)
        {
            return GetLoggerFactory(app).AddLog4Net(log4NetConfigFile);
        }

        /// <summary>
        /// Adds Log4Net to the IServiceProvider request execution.
        /// </summary>
        /// <param name="app">The IServiceProvider.</param>
        /// <param name="log4NetConfigFile">Set log4net config file path</param>
        /// <param name="watch">Set boolean value to watch</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static ILoggerFactory UseLog4Net(this IServiceProvider app, string log4NetConfigFile, bool watch)
        {
            return GetLoggerFactory(app).AddLog4Net(log4NetConfigFile, watch);
        }

        /// <summary>
        /// TO get ILoggerFactory object 
        /// </summary>
        /// <param name="app">The IServiceProvider.</param>
        private static ILoggerFactory GetLoggerFactory(IServiceProvider app)
        {
            return app.GetService<ILoggerFactory>();
        }

    }
}

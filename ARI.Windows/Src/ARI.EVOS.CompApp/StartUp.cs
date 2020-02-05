using ARI.EVOS.CompApp.ExtensionMethod;
using ARI.EVOS.Infra.Service;
using Chassis.Logger.ExtensionMethods;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace ARI.EVOS.CompApp
{
    /// <summary>
    /// Start up class of the application 
    /// </summary>
    public class StartUp
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; private set; }

        public StartUp()
        {
            StartApplication();
        }

        private void StartApplication()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();

            var services = new ServiceCollection();
            services.AddExtensionServices(options =>
            {
                options.DBConnectionString = Configuration["AppSettings:DBConnectionString"];
            });
            services.AddLogging();
            ServiceProvider = services.BuildServiceProvider();
            ServiceLocator.AddProvider(ServiceProvider);
            ServiceProvider.UseLog4Net();
        }
    }
}

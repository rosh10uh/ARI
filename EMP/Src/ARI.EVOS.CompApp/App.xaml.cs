using ARI.EVOS.CompApp.ExtensionMethod;
using ARI.EVOS.Infra;
using ARI.EVOS.Infra.Service;
using Chassis.Logger.ExtensionMethods;
using EMP.Management;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using Prism.Unity.Extensions;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace ARI.EVOS.CompApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; private set; }        

        /// <summary>
        /// Extended service provider
        /// </summary>
        /// <returns></returns>
        protected override IContainerExtension CreateContainerExtension()
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

            var prismContainer = PrismContainerExtension.Current;
            ServiceProvider = prismContainer.CreateServiceProvider(services);
            ServiceProvider.UseLog4Net();           

            return prismContainer;
        }       

        /// <summary>
        /// Create window shell
        /// </summary>
        /// <returns></returns>
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }        

        /// <summary>
        /// Configure Modules
        /// </summary>
        /// <param name="moduleCatalog"></param>
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<EmployeeModule>();                        
        }

        /// <summary>
        /// Creates the IModuleCatalog used by Prism
        /// </summary>
        protected override IModuleCatalog CreateModuleCatalog()
        {
            return base.CreateModuleCatalog();
        }

        /// <summary>
        /// Register container dependency
        /// </summary>
        /// <param name="containerRegistry"></param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
               
        /// <summary>
        /// Global Exception Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            RemoveStatusBarMessage();
            ShowExceptionMessageBox(e);
            e.Handled = true;
            var logger = Container.Resolve<ILogger<App>>();
            logger.LogError(e.Exception.Message);
        }        

        /// <summary>
        /// Show Exception Message
        /// </summary>
        /// <param name="e"></param>
        private static void ShowExceptionMessageBox(System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, BaseConstant.ExceptionMessageBoxTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        /// <summary>
        /// Remove status bar message
        /// </summary>
        private void RemoveStatusBarMessage()
        {
            var eventAggregator = Container.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<MessageSentEvent>().Publish("");
        }

        /// <summary>
        /// Allows the keyboard to bring the items into view as expected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {            
            if (Keyboard.IsKeyDown(Key.Down) || Keyboard.IsKeyDown(Key.Up))
                return;

            e.Handled = true;
        }        
    }
}

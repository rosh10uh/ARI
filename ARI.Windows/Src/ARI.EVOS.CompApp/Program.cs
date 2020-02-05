using ARI.EVOS.CompApp.Forms;
using ARI.EVOS.Infra;
using ARI.EVOS.Infra.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Windows.Forms;

namespace ARI.EVOS.CompApp
{
    /// <summary>
    /// Entry point of application 
    /// </summary>
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            _ = new StartUp();
            Application.Run(new MdiForm());
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            ShowExceptionDetails(e.Exception);
        }

        /// <summary>
        /// Used for show the exception message and write the log
        /// </summary>
        /// <param name="Ex"></param>
        private static void ShowExceptionDetails(Exception ex)
        {
            var logger = ServiceLocator.InstanceProvider.GetRequiredService<ILogger<MdiForm>>();
            logger.LogError(ex.Message);
            System.Windows.MessageBox.Show(ex.Message, BaseConstant.ExceptionMessageBoxTitle, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
        }
    }
}

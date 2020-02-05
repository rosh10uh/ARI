using ARI.EVOS.Infra.Service;
using Microsoft.Extensions.Logging;
using Prism.Events;
using Prism.Mvvm;
using System.Windows;

namespace ARI.EVOS.Infra.ViewModel
{
    /// <summary>
    /// Base class to set basic thing for view model
    /// </summary>
    public abstract class BaseViewModel : BindableBase
    {
        private readonly ILogger _logger;
        protected readonly IEventAggregator _eventAggregator;

        protected BaseViewModel(IEventAggregator eventAggregator, ILogger logger)
        {        
            _eventAggregator = eventAggregator;
            _logger = logger;
        }       

        /// <summary>
        /// Display error message into message box
        /// </summary>
        /// <param name="errorMessage"></param>
        protected virtual void ShowValidationMessage(string errorMessage)
        {
            ShowStatusBarMessage();
            MessageBox.Show(errorMessage, BaseConstant.ValidationMessageBoxTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        /// <summary>
        /// Display status bar message
        /// </summary>
        /// <param name="message"></param>
        protected virtual void ShowStatusBarMessage(string message = null)
        {
            _eventAggregator.GetEvent<MessageSentEvent>().Publish(message);           
        }

        /// <summary>
        /// Set window title
        /// </summary>
        /// <param name="title"></param>
        protected virtual void SetWindowTitle(string title)
        {
            
            if (Application.Current?.MainWindow != null)
            {
                Application.Current.MainWindow.Title = title;
            }
        }

        /// <summary>
        /// Log Information
        /// </summary>
        /// <param name="message"></param>
        protected virtual void LogInformation(string message)
        {
            _logger.LogInformation(message);
        }

        /// <summary>
        /// Log Warning
        /// </summary>
        /// <param name="message"></param>
        protected virtual void LogWarning(string message)
        {
            _logger.LogWarning(message);
        }
    }
}

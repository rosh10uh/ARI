using ARI.EVOS.Common.Models;
using ARI.EVOS.Infra.Service;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Globalization;
using System.Windows.Threading;

namespace ARI.EVOS.Common.ViewModels
{
    /// <summary>
    /// Map with common UI screen.(StatusbarView)
    /// </summary>
    public class StatusbarViewModel : BindableBase
    {
        private DispatcherTimer _timer = null;
        private StatusModel _statusbarModel;
        private readonly IEventAggregator _eventAggregator;
        public StatusModel Statusbar
        {
            get => _statusbarModel;
            set => SetProperty(ref _statusbarModel, value);
        }

        private string _time;
        public string Time
        {
            get => _time;
            set => SetProperty(ref _time, value);
        }

        private string _date;
        public string Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        private string _setMessage;
        public string Message
        {
            get => _setMessage;
            set => SetProperty(ref _setMessage, value);
        }

        public StatusbarViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<MessageSentEvent>().Subscribe(MessageReceived);
            StartTimer();
        }  
        
        /// <summary>
        /// Display time in statusbar
        /// </summary>
        public void StartTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += new EventHandler(TimerTick);
            _timer.Start();
        }

        /// <summary>
        /// Set date and time format in statusbar
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void TimerTick(object send, EventArgs e)
        {
            this.Time = DateTime.Now.ToString("HH:mm tt");
            this.Date = DateTime.Now.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture); 
        }

        /// <summary>
        /// Set message in statusbar
        /// </summary>
        /// <param name="message"></param>
        private void MessageReceived(string message)
        {
            Message = message;
        }
    }
}

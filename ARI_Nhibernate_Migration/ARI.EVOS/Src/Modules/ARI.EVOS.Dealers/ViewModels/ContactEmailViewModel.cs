using ARI.EVOS.Dealer.AppServices.Interface;
using ARI.EVOS.Dealers.Models;
using ARI.EVOS.Dealers.Views;
using ARI.EVOS.Infra.ViewModel;
using Microsoft.Extensions.Logging;
using Prism.Commands;
using Prism.Events;
using System.Windows;
using ARI.EVOS.Dealers.Constant;

namespace ARI.EVOS.Dealers.ViewModels
{
    /// <summary>
    /// Map with Contact Email UI screen(View Model)
    /// </summary>
    public class ContactEmailViewModel : BaseViewModel
    {
        private readonly IDealerNetwork _dealerNetwork;
                
        private DealerNetworkModel _dealerNetworkModel;
        private ContactEmailModel _contactEmailModel;
        public ContactEmailModel ContactEmail
        {
            get => _contactEmailModel;
            set => SetProperty(ref _contactEmailModel, value);
        }

        public DelegateCommand SaveEmailCommand { get; private set; }
        public DelegateCommand CancelEmailCommand { get; private set; }        

        public ContactEmailViewModel(IDealerNetwork dealerNetwork, ILogger<ContactEmailViewModel> logger,
                                    DealerNetworkModel dealerNetworkModel, IEventAggregator eventAggregator) : base(eventAggregator, logger)
        {
            _dealerNetworkModel = dealerNetworkModel;
            _dealerNetwork = dealerNetwork;
            _contactEmailModel = dealerNetworkModel.ContactEmail;
            ContactEmailOnLoad();
        }

        /// <summary>
        /// To load basic requirements
        /// </summary>
        private void ContactEmailOnLoad()
        {
            ContactEmail = new ContactEmailModel();
            SaveEmailCommand = new DelegateCommand(SaveEmail);
            CancelEmailCommand = new DelegateCommand(CancelEmail);
            _eventAggregator.GetEvent<DealerAggregator>().Subscribe(DealersNetworkReceived);
        }        

        /// <summary>
        /// This method is used to save additional email details of dealer.
        /// </summary>
        private async void SaveEmail()
        {            
            LogInformation(ContactEmailConstant.ExecutingInsert);
            ContactEmail.DealerId = _dealerNetworkModel.DealerId;
            ContactEmail.MakeCode = _dealerNetworkModel.MakeCode;
            ContactEmail.CountryCode = _dealerNetworkModel.CountryCode;
            var insertData = await _dealerNetwork.SaveEmail(ContactEmail);
            if (insertData.IsSuccess)
            {                
                LogInformation(ContactEmailConstant.ExecutedInsert);
                CancelEmail();
            }
            else
            {
                LogWarning(ContactEmailConstant.FailedInsert);
            }
        }

        /// <summary>
        /// This method is used to retrieve dealer network model parameters.
        /// </summary>
        /// <param name="dealersNetwork"></param>
        private void DealersNetworkReceived(DealerNetworkModel dealersNetwork)
        {
            _dealerNetworkModel = dealersNetwork;
            ContactEmail = _dealerNetworkModel.ContactEmail;
        }

        /// <summary>
        /// This method is used to perform the actions related to cancel button.
        /// </summary>
        private void CancelEmail()
        {
            ContactEmail = new ContactEmailModel();
            foreach (Window window in Application.Current.Windows)
            {
                if (window is ContactEmailView)
                {
                    window.Close();
                }
            }
        }
    }
}

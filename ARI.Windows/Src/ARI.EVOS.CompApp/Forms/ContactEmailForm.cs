using ARI.EVOS.CompApp.Constant;
using ARI.EVOS.Dealer.AppServices.Interface;
using ARI.EVOS.Dealers.Models;
using ARI.EVOS.Infra.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARI.EVOS.CompApp.Forms
{
    /// <summary>
    /// This class is used as a Additional Email UI Screen and map with Contact Email Model
    /// </summary>
    public partial class ContactEmailForm : BaseForm
    {
        private readonly IDealerNetwork _dealerNetwork;

        private DealerNetworkModel _dealerNetworkModel;
        private ContactEmailModel _contactEmailModel;
        public ContactEmailForm(IDealerNetwork dealerNetwork, DealerNetworkModel dealerNetworkModel, ContactEmailModel contactEmailModel, ILogger<ContactEmailForm> logger, IMessage message) : base(logger, message)
        {
            InitializeComponent();
            _dealerNetwork = dealerNetwork;
            _dealerNetworkModel = dealerNetworkModel;
            _contactEmailModel = contactEmailModel;
            ContactEmailOnLoad();
        }

        /// <summary>
        /// This event is used for save email
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnClose_Click(object sender, EventArgs e)
        {
            await SaveEmail();
        }

        /// <summary>
        /// This event is used for cancel email
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            CancelEmail();
        }

        /// <summary>
        /// To load basic requirements
        /// </summary>
        private void ContactEmailOnLoad()
        {
            _contactEmailModel.DealerId = _dealerNetworkModel.DealerId;
            _contactEmailModel.CountryCode = _dealerNetworkModel.CountryCode;
            _contactEmailModel.MakeCode = _dealerNetworkModel.MakeCode;
            txtStockName.Text = _contactEmailModel.StockContactName;
            txtStockEmail.Text = _contactEmailModel.StockContactEmail;
            txtFinanceName.Text = _contactEmailModel.FinanceContactName;
            txtFinanceEmail.Text = _contactEmailModel.FinanceContactEmail;
            txtLicenseName.Text = _contactEmailModel.LicenseContactName;
            txtLicenceEmail.Text = _contactEmailModel.LicenseContactEmail;
            this.Text = DealerConstant.ContactEmailTitle;
        }


        /// <summary>
        /// Save additional email details of dealer
        /// </summary>
        private async Task SaveEmail()
        {
            MapContactDetail();
            LogInformation(DealerConstant.ExecutingInsertEmail);
            var insertData = await _dealerNetwork.SaveEmail(_contactEmailModel);
            if (insertData.IsSuccess)
            {
                LogInformation(DealerConstant.ExecutedInsertEmail);
                CancelEmail();
            }
            else
            {
                LogWarning(DealerConstant.FailedInsertEmail);
            }
        }

        /// <summary>
        /// Close Contact Email screen
        /// </summary>
        private void CancelEmail()
        {
            this.Close();
        }

        /// <summary>
        /// Map contact email details to Contact email model
        /// </summary>
        private void MapContactDetail()
        {
            _contactEmailModel.StockContactName = txtStockName.Text.Trim();
            _contactEmailModel.StockContactEmail = txtStockEmail.Text.Trim();
            _contactEmailModel.FinanceContactName = txtFinanceName.Text.Trim();
            _contactEmailModel.FinanceContactEmail = txtFinanceEmail.Text.Trim();
            _contactEmailModel.LicenseContactName = txtLicenseName.Text.Trim();
            _contactEmailModel.LicenseContactEmail = txtLicenceEmail.Text.Trim();
        }

        /// <summary>
        /// Apply common button style
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonStyle(object sender, PaintEventArgs e)
        {
            ApplyButtonStyle((Button)sender, e);
        }

    }
}

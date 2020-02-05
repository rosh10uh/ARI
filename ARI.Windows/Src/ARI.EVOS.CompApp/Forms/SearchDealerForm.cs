using ARI.EVOS.Common.Models;
using ARI.EVOS.CompApp.Constant;
using ARI.EVOS.Dealer.AppServices.Interface;
using ARI.EVOS.Dealers.Models;
using ARI.EVOS.Infra;
using ARI.EVOS.Infra.Interface;
using ARI.EVOS.Infra.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARI.EVOS.CompApp.Forms
{
    /// <summary>
    ///  This class is used as a Search Dealer UI Screen and map with Dealer Search Model
    /// </summary>
    public partial class SearchDealerForm : BaseForm
    {
        private readonly IMasterData _masterData;
        private readonly IDealerNetwork _dealerNetwork;

        private readonly List<MakeModel> _makes = new List<MakeModel>();
        private List<MakeModel> makes = new List<MakeModel>();
        private DealerSearchModel _dealerSearchModel;
        private DealerNetworkModel _dealerNetworkModel;

        public SearchDealerForm(IDealerNetwork dealerNetwork, IMasterData masterData, ILogger<SearchDealerForm> logger, IMessage message) : base(logger, message)
        {
            InitializeComponent();
            _dealerNetwork = dealerNetwork;
            _masterData = masterData;
        }

        /// <summary>
        /// This event is used to load the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchDealerForm_Load(object sender, EventArgs e)
        {
            ((MdiForm)(MdiParent)).Text = DealerConstant.DealerSearchTitle;
            Task.FromResult(GetCountryDetail());
            Task.FromResult(GetMakeDetails());
            Task.FromResult(BindDealerData());
            ShowStatusBarMessage(DealerConstant.SearchDealer);
            Location = new Point(Screen.PrimaryScreen.Bounds.X,
                         Screen.PrimaryScreen.Bounds.Y);
            panelDealer.Left = (ClientSize.Width - panelDealer.Width) / 2;
            panelDealer.Top = (ClientSize.Height - panelDealer.Height) / 2;
            cmbCountryCode.Select();
            cmbCountryCode.Focus();
        }

        /// <summary>
        /// This event is used to change the make details as country's selected value changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbCountryCode_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbCountryCode.SelectedValue.ToString() != null)
            {
                BindMakeByCountryCode();
            }
        }

        /// <summary>
        /// This event is used to view dealers based on search condition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            Task.FromResult(ViewDealers());
        }

        /// <summary>
        /// This event is used to get dealer details based on search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvSearchDealer_Click(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                ShowStatusBarMessage();
                Task.FromResult(GetDealer());
            }
        }

        /// <summary>
        /// This event is used to navigate to dealer screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddDealer_Click(object sender, EventArgs e)
        {
            _dealerNetworkModel = new DealerNetworkModel();
            var frmDealerNetwork = ActivatorUtilities.CreateInstance(ServiceLocator.InstanceProvider, typeof(DealerNetworkForm), _dealerNetworkModel);

            ((DealerNetworkForm)frmDealerNetwork).StartPosition = FormStartPosition.CenterScreen;
            ((DealerNetworkForm)frmDealerNetwork).Text = DealerConstant.DealerNetworkTitle;
            ((DealerNetworkForm)frmDealerNetwork).MdiParent = MdiParent;
            ShowStatusBarMessage(DealerConstant.AddingDealer);
            ((DealerNetworkForm)frmDealerNetwork).Show();
            Close();
        }

        /// <summary>
        /// This event is used to reset controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClearSelection_Click(object sender, EventArgs e)
        {
            ShowStatusBarMessage(DealerConstant.SearchDealer);
            Reset();
        }

        /// <summary>
        /// This event is used to exit from search dealer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        /// <summary>
        /// This event is used to clear default selection row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvSearchDealer_RowsAdded(object sender, System.EventArgs e)
        {
            if (dgvSearchDealer.CurrentRow != null)
            {
                dgvSearchDealer.CurrentRow.Cells[0].Selected = false;
            }
        }

        /// <summary>
        ///  Navigate focus on control with enter key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NevigateWithEnterKey(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Task.FromResult(ViewDealers());
            }
        }
        /// <summary>
        /// Bind dealer details
        /// </summary>
        /// <returns></returns>
        private async Task BindDealerData()
        {
            var dealers = await _dealerNetwork.GetDealersList();
            dgvSearchDealer.DataSource = dealers.Value;
            if (dgvSearchDealer.Rows.Count == 0)
            {
                dgvSearchDealer.Enabled = false;
                dgvSearchDealer.ColumnHeadersVisible = false;
                dgvSearchDealer.DataSource = null;
            }
            else
            {
                BindGrid();                
            }
        }

        /// <summary>
        /// Get country detail from data source
        /// </summary>
        private async Task GetCountryDetail()
        {
            var countryData = await _masterData.GetCountryDetail();
            countryData.Value.Insert(0, new CountryModel { CountryCode = "", CountryName = "" });
            cmbCountryCode.DataSource = countryData.Value;
            cmbCountryCode.ValueMember = "CountryCode";
            cmbCountryCode.DisplayMember = "CountryName";
        }
        /// <summary>
        /// Get make detail from data source
        /// </summary>
        /// <returns></returns>
        private async Task GetMakeDetails()
        {
            var makeData = await _masterData.GetMakeDetail();
            _makes.AddRange(makeData.Value);
            makes = new List<MakeModel>(_makes);
        }

        /// <summary>
        /// Bind make based on selected country code
        /// </summary>
        private void BindMakeByCountryCode()
        {
            BindMakeByCountry(makes, cmbCountryCode, cmbMake);
        }

        /// <summary>
        /// View dealer details
        /// </summary>
        /// <returns></returns>
        private async Task ViewDealers()
        {
            SetSearchDealer();
            var dealers = await _dealerNetwork.SearchDealers(_dealerSearchModel);
            if (dealers.HasValue && dealers.Value.Count > 0)
            {
                dgvSearchDealer.DataSource = dealers.Value;
                BindGrid();
            }
            else
            {
                dgvSearchDealer.Enabled = false;
                dgvSearchDealer.ColumnHeadersVisible = false;
                dgvSearchDealer.DataSource = null;
                ShowValidationMessage(DealerConstant.RecordsNotFound);
            }
        }

        /// <summary>
        /// Bind the dealer data in grid
        /// </summary>
        private void BindGrid()
        {
            dgvSearchDealer.ColumnHeadersDefaultCellStyle.BackColor = this.BackColor;
            dgvSearchDealer.EnableHeadersVisualStyles = false;
            dgvSearchDealer.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvSearchDealer.AllowUserToResizeRows = false;
            dgvSearchDealer.RowTemplate.Height = 18;

            dgvSearchDealer.Columns["CountryCode"].Visible = false;
            dgvSearchDealer.Columns["MakeCode"].Visible = false;
            dgvSearchDealer.Columns["DealerId"].HeaderText = "Dealer";
            dgvSearchDealer.Columns["Make"].HeaderText = "Make";
            dgvSearchDealer.Columns["VendorId"].HeaderText = "Vendor Id";
            dgvSearchDealer.Columns["VendorName"].HeaderText = "Name";
            dgvSearchDealer.Columns["City"].HeaderText = "City";
            dgvSearchDealer.Columns["State"].HeaderText = "State";
            dgvSearchDealer.Columns["ZipCode"].HeaderText = "Zip";
            dgvSearchDealer.Columns["SellingDelivInd"].HeaderText = "S/D/B";
            dgvSearchDealer.Columns["DealerRating"].HeaderText = "Rating";
            dgvSearchDealer.Columns["DealerComments"].HeaderText = "Comments";

            dgvSearchDealer.Columns["DealerId"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvSearchDealer.Columns["Make"].Width = 170;
            dgvSearchDealer.Columns["VendorId"].Width = 90;
            dgvSearchDealer.Columns["VendorName"].Width = 220;
            dgvSearchDealer.Columns["City"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvSearchDealer.Columns["State"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvSearchDealer.Columns["ZipCode"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvSearchDealer.Columns["SellingDelivInd"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvSearchDealer.Columns["DealerRating"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvSearchDealer.Columns["DealerComments"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvSearchDealer.Enabled = true;
            dgvSearchDealer.ColumnHeadersVisible = true;
            dgvSearchDealer.RowHeadersVisible = false;
            dgvSearchDealer.ReadOnly = true;            
        }

        /// <summary>
        /// Get dealer details
        /// </summary>
        /// <returns></returns>
        private async Task GetDealer()
        {
            _dealerNetworkModel = new DealerNetworkModel();
            _dealerNetworkModel.CountryCode = dgvSearchDealer.CurrentRow.Cells["CountryCode"].Value.ToString();
            _dealerNetworkModel.MakeCode = dgvSearchDealer.CurrentRow.Cells["MakeCode"].Value.ToString();
            _dealerNetworkModel.DealerId = dgvSearchDealer.CurrentRow.Cells["DealerId"].Value.ToString();
            dgvSearchDealer.CurrentRow.Selected = true;
            var bindDealer = await _dealerNetwork.GetDealerNetwork(_dealerNetworkModel);
            _dealerNetworkModel = bindDealer.Value.FirstOrDefault();
            var frmDealerNetwork = ActivatorUtilities.CreateInstance(ServiceLocator.InstanceProvider, typeof(DealerNetworkForm), _dealerNetworkModel);
            MapDealerDetail((DealerNetworkForm)frmDealerNetwork, _dealerNetworkModel);
            ((DealerNetworkForm)frmDealerNetwork).StartPosition = FormStartPosition.CenterScreen;
            ((DealerNetworkForm)frmDealerNetwork).Text = DealerConstant.DealerNetworkTitle;
            ((DealerNetworkForm)frmDealerNetwork).MdiParent = MdiParent;
            ((DealerNetworkForm)frmDealerNetwork).Show();
            Close();
        }

        /// <summary>
        /// Set dealer details
        /// </summary>
        private void SetSearchDealer()
        {
            _dealerSearchModel = new DealerSearchModel();
            _dealerSearchModel.CountryCode = cmbCountryCode.SelectedValue.ToString();
            _dealerSearchModel.MakeCode = cmbMake.SelectedValue.ToString();
            _dealerSearchModel.DealerId = txtDealerId.Text;
            _dealerSearchModel.VendorName = txtName.Text;
        }

        /// <summary>
        /// Map dealer details to Dealer Network Model
        /// </summary>
        /// <param name="frmDealerNetwork"></param>
        /// <param name="dealerNetworkModel"></param>
        private void MapDealerDetail(DealerNetworkForm frmDealerNetwork, DealerNetworkModel dealerNetworkModel)
        {
            DealerInfo(frmDealerNetwork, dealerNetworkModel);
            DealerContactInfo(frmDealerNetwork, dealerNetworkModel);
            DealerAddressInfo(frmDealerNetwork, dealerNetworkModel);
            DelaerProgramInfo(frmDealerNetwork, dealerNetworkModel);
            DealerOtherInfo(frmDealerNetwork, dealerNetworkModel);
            frmDealerNetwork.btnCopyDealer.Enabled = false;
            frmDealerNetwork.btnUpdate.Enabled = true;
        }

        /// <summary>
        /// Set Dealer information to dealer model
        /// </summary>
        private void DealerInfo(DealerNetworkForm frmDealerNetwork, DealerNetworkModel dealerNetworkModel)
        {
            var countryData = _masterData.GetCountryDetail();
            countryData.Result.Value.Insert(0, new CountryModel { CountryCode = "", CountryName = "" });
            frmDealerNetwork.cmbCountryCode.DataSource = countryData.Result.Value;
            frmDealerNetwork.cmbCountryCode.ValueMember = "CountryCode";
            frmDealerNetwork.cmbCountryCode.DisplayMember = "CountryName";
            frmDealerNetwork.cmbCountryCode.SelectedValue = dealerNetworkModel.CountryCode;

            var makedetails = _masterData.GetMakeDetail().Result.Value.Where(x => x.CountryCode == dealerNetworkModel.CountryCode).Select(x => x);
            makes.Insert(0, new MakeModel { CountryCode = "", MakeCode = "", MakeDescription = "" });
            frmDealerNetwork.cmbMake.DataSource = makedetails.ToList();
            frmDealerNetwork.cmbMake.ValueMember = "MakeCode";
            frmDealerNetwork.cmbMake.DisplayMember = "MakeDescription";
            frmDealerNetwork.cmbMake.SelectedValue = dealerNetworkModel.MakeCode;

            frmDealerNetwork.txtDealerId.Text = dealerNetworkModel.DealerId;
            frmDealerNetwork.txtVendorId.Text = dealerNetworkModel.VendorId;
            frmDealerNetwork.txtName.Text = dealerNetworkModel.VendorName;
        }

        /// <summary>
        /// Set program information to dealer model
        /// </summary>
        private void DelaerProgramInfo(DealerNetworkForm frmDealerNetwork, DealerNetworkModel dealerNetworkModel)
        {
            frmDealerNetwork.txtProgram.Text = dealerNetworkModel.CreationPrgm;
            frmDealerNetwork.txtUser.Text = dealerNetworkModel.CreationUser;
            frmDealerNetwork.txtCreationDate.Text = GetValidDate(dealerNetworkModel.CreationDate.ToString());
            frmDealerNetwork.txtLastUsed.Text = GetValidDate(dealerNetworkModel.LastUsedDate.ToString());
            frmDealerNetwork.txtLastProgram.Text = dealerNetworkModel.LastPrgm;
            frmDealerNetwork.txtLastUser.Text = dealerNetworkModel.LastUser;
            frmDealerNetwork.txtLastChg.Text = GetValidDate(dealerNetworkModel.LastChg.ToString());
        }

        /// <summary>
        /// Set Dealer address information to dealer model
        /// </summary>
        private void DealerAddressInfo(DealerNetworkForm frmDealerNetwork, DealerNetworkModel dealerNetworkModel)
        {
            frmDealerNetwork.txtAddress1.Text = dealerNetworkModel.Address1;
            frmDealerNetwork.txtAddress2.Text = dealerNetworkModel.Address2;
            frmDealerNetwork.txtAddress3.Text = dealerNetworkModel.Address3;
            frmDealerNetwork.txtAddress4.Text = dealerNetworkModel.Address4;
            frmDealerNetwork.txtCity.Text = dealerNetworkModel.City;
            frmDealerNetwork.txtState.Text = dealerNetworkModel.State;
            frmDealerNetwork.txtZipCode.Text = dealerNetworkModel.ZipCode;
            frmDealerNetwork.txtZipCity.Text = dealerNetworkModel.ZipPlus4;
        }

        /// <summary>
        /// Set Dealer contact information to dealer model
        /// </summary>
        private void DealerContactInfo(DealerNetworkForm frmDealerNetwork, DealerNetworkModel dealerNetworkModel)
        {
            frmDealerNetwork.txtContact1.Text = dealerNetworkModel.Contact1;
            frmDealerNetwork.txtContact2.Text = dealerNetworkModel.Contact2;
            frmDealerNetwork.txtPhonec1.Text = dealerNetworkModel.Phone1Exchange;
            frmDealerNetwork.txtPhone1.Text = dealerNetworkModel.Phone1Number;
            frmDealerNetwork.txtPhonec2.Text = dealerNetworkModel.Phone2Exchange;
            frmDealerNetwork.txtPhone2.Text = dealerNetworkModel.Phone2Number;
            frmDealerNetwork.txtFax.Text = dealerNetworkModel.FaxNumber;
            frmDealerNetwork.txtEmail.Text = dealerNetworkModel.Email;

        }

        /// <summary>
        /// Set Dealer other information to dealer model
        /// </summary>
        private void DealerOtherInfo(DealerNetworkForm frmDealerNetwork, DealerNetworkModel dealerNetworkModel)
        {
            frmDealerNetwork.txtBacCode.Text = dealerNetworkModel.GmBusnAsctCd;
            frmDealerNetwork.txtSelling.Text = dealerNetworkModel.SellingDelivInd;
            frmDealerNetwork.txtMinority.Text = dealerNetworkModel.MinorityInd;
            frmDealerNetwork.txtDealer.Text = dealerNetworkModel.DealerRating;
            frmDealerNetwork.txtMfg.Text = dealerNetworkModel.MfgZoneCode;
            frmDealerNetwork.txtDraft.Text = dealerNetworkModel.PrintDealerDraft;
            frmDealerNetwork.txtDraft.Text = dealerNetworkModel.DraftAcct1;
            frmDealerNetwork.txtDraftAcc.Text = dealerNetworkModel.DraftAcct2;
            frmDealerNetwork.txtTaxId.Text = dealerNetworkModel.TaxId;
            frmDealerNetwork.txtPayToVendor.Text = dealerNetworkModel.PayToVendor;
            frmDealerNetwork.txtFactoryCount.Text = dealerNetworkModel.FactoryCountPrior12.ToString();
            frmDealerNetwork.txtStock.Text = dealerNetworkModel.StockCountPrior12.ToString();
            frmDealerNetwork.txtPaymentVia.Text = dealerNetworkModel.PymtVia;
            frmDealerNetwork.cmbPrintNotice.Text = dealerNetworkModel.CourtesyDelivPrintInd;
            frmDealerNetwork.cmbTerms.Text = dealerNetworkModel.TermsOverride;
            frmDealerNetwork.txtCertificate.Text = dealerNetworkModel.CertInd;
            frmDealerNetwork.txtBankNo1.Text = dealerNetworkModel.BankNumber;
            frmDealerNetwork.txtBankName.Text = dealerNetworkModel.BankName;
            frmDealerNetwork.txtBankCity.Text = dealerNetworkModel.BankCity;
            frmDealerNetwork.txtBankAcct.Text = dealerNetworkModel.BankAccount;
            frmDealerNetwork.txtCommission.Text = dealerNetworkModel.UkCommission.ToString();
            frmDealerNetwork.txtDealerComment.Text = dealerNetworkModel.DealerComments;
            frmDealerNetwork.txtClient1.Text = dealerNetworkModel.KdClient;
            frmDealerNetwork.txtDiv.Text = dealerNetworkModel.KdDiv;
            frmDealerNetwork.txtFactory.Text = dealerNetworkModel.CanVolumeFactory.ToString();
            frmDealerNetwork.txtStock1.Text = dealerNetworkModel.CanVolumeStock.ToString();
        }

        /// <summary>
        /// Validate the date
        /// </summary>
        /// <param name="date">string</param>
        /// <returns>string</returns>
        private string GetValidDate(string date)
        {
            if (!string.IsNullOrEmpty(date))
            {
                return Convert.ToDateTime(date).ToString(BaseConstant.DateFormat);
            }
            return null;
        }

        /// <summary>
        /// Reset controls
        /// </summary>
        private void Reset()
        {
            txtDealerId.Text = string.Empty;
            txtName.Text = string.Empty;
            cmbCountryCode.Text = string.Empty;
            cmbMake.Text = string.Empty;
            Task.FromResult(BindDealerData());
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

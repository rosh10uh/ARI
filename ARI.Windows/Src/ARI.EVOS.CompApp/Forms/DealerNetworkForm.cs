using ARI.EVOS.Common.Models;
using ARI.EVOS.CompApp.Constant;
using ARI.EVOS.Dealer.AppServices.Interface;
using ARI.EVOS.Dealer.Domain.SharedKernel;
using ARI.EVOS.Dealers.Models;
using ARI.EVOS.Infra;
using ARI.EVOS.Infra.Interface;
using ARI.EVOS.Infra.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace ARI.EVOS.CompApp.Forms
{
    /// <summary>
    ///  This class is used as a Dealer UI Screen and map with Dealer Network Model
    /// </summary>
    public partial class DealerNetworkForm : BaseForm
    {
        private readonly IDealerNetwork _dealerNetwork;
        private readonly IMasterData _masterData;

        private DealerNetworkModel _dealerNetworkModel;
        private ContactEmailModel _contactEmailModel;

        private readonly List<MakeModel> _makes = new List<MakeModel>();
        private List<MakeModel> makes = new List<MakeModel>();
        private GetReadyModel _getReadyModel;
        private bool _isGREdit = false;

        public DealerNetworkForm(IDealerNetwork dealer, IMasterData masterData, ILogger<DealerNetworkForm> logger, IMessage message, DealerNetworkModel dealerNetworkModel) : base(logger, message)
        {
            SuspendLayout();
            InitializeComponent();
            ResumeLayout();
            _dealerNetwork = dealer;
            _masterData = masterData;
            _dealerNetworkModel = dealerNetworkModel;
        }

        /// <summary>
        /// This event is used to load the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DealerNetworkForm_Load(object sender, EventArgs e)
        {
            if (_dealerNetworkModel.CountryCode == null)
            {
                DealerOnLoad();
            }
            else
            {
                Task.FromResult(GetReadyDetails());
            }
            ((MdiForm)(MdiParent)).Text = DealerConstant.DealerNetworkTitle;
            panelDealer.Left = (Size.Width - panelDealer.Width) / 2;
            panelDealer.Top = (Size.Height - panelDealer.Height) / 2;
            cmbCountryCode.Select();
            cmbCountryCode.Focus();
        }

        /// <summary>
        /// This event is used to copy dealer details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnCopyDealer_Click(object sender, EventArgs e)
        {
            await InsertDealer();
        }

        /// <summary>
        /// This event is used to update dealer details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnUpdate_Click(object sender, EventArgs e)
        {
            await UpdateDealer();
        }

        /// <summary>
        /// This event is used to delete dealer details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnDelete_Click(object sender, EventArgs e)
        {
            await DeleteDealer();
        }

        /// <summary>
        /// This event is used to change the make details as country's selected value changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbCountryCode_SelectedValueChanged(object sender, EventArgs e)
        {
            Task.FromResult(GetMakeDetail());
            CountryChanged();
        }

        /// <summary>
        /// This event is used to reset dealer details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            ResetDealer();
            ResetDealerButton();
            ShowStatusBarMessage();
            Task.FromResult(GetCountryDetail());
            Task.FromResult(GetMakeDetail());
        }

        /// <summary>
        /// Select a record from grid and fill the text value
        /// </summary>
        private void DgReady_Click(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                txtClient.Text = dgReady.CurrentRow.Cells["ClientId"].Value.ToString();
                txtAmount.Text = dgReady.CurrentRow.Cells["GetReadyAmount"].Value== null ?string.Empty: dgReady.CurrentRow.Cells["GetReadyAmount"].Value.ToString();

                _getReadyModel.GetReadyCategory = dgReady.CurrentRow.Cells["GetReadyCategory"].Value.ToString();
                dgReady.CurrentRow.Selected = true;
                BindGetReadyDetail(_getReadyModel);
            }
        }

        /// <summary>
        /// Add get ready
        /// </summary>
        private async void BtnAddGetReady_Click(object sender, EventArgs e)
        {
            if (IsValidateGetReady())
            {
                ShowStatusBarMessage(DealerConstant.InsertingGetReadyRecord);
                LogInformation(DealerConstant.ExecutingGetReadyInsert);
                SetCompositeKey();
                SetGetReadyDetails();
                SetEffectiveDate();
                await SetInsertGetReadyProcess();
            }
        }

        /// <summary>
        /// Update get ready
        /// </summary>
        private async void BtnUpdateGetReady_Click(object sender, EventArgs e)
        {
            if (IsValidateGetReady())
            {
                _isGREdit = false;
                dgReady.Enabled = false;
                ShowStatusBarMessage(DealerConstant.UpdatingGetReadyRecord);
                LogInformation(DealerConstant.ExecutingGetReadyUpdate);
                SetGetReadyDetails();
                SetEffectiveDate();
                await SetUpdateGetReady();
            }
        }

        /// <summary>
        /// Delete get ready
        /// </summary>
        private async void BtnDeleteGetReady_Click(object sender, EventArgs e)
        {
            if (IsValidateGetReady())
            {
                SetGetReadyDetails();

                if (_getReadyModel != null && string.IsNullOrEmpty(_getReadyModel.GetReadyCategory))
                {
                    ShowValidationMessage(DealerConstant.DeletedGetReadyRecordFailed);
                }
                else
                {
                    var messageBoxResult = _message.Show(BaseConstant.DeleteConfirmation, BaseConstant.DeleteConfirmationTitle, MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        _isGREdit = false;
                        dgReady.Enabled = false;
                        ShowStatusBarMessage(DealerConstant.DeletingGetReadyRecord);
                        LogInformation(DealerConstant.ExecutingGetReadyDelete);
                        await SetDeleteGetReadyProcess();
                    }
                }
            }
        }

        /// <summary>
        /// This event is used to close dealer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            ShowStatusBarMessage();
            var frmSearchDealer = ActivatorUtilities.CreateInstance(ServiceLocator.InstanceProvider, typeof(SearchDealerForm));
            ((SearchDealerForm)frmSearchDealer).StartPosition = FormStartPosition.CenterParent;
            ((SearchDealerForm)frmSearchDealer).MdiParent = MdiParent;
            ((SearchDealerForm)frmSearchDealer).Show();
            Close();
        }

        /// <summary>
        /// This event is used to get city from zip
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnZipCity_Click(object sender, EventArgs e)
        {
            Task.FromResult(GetCityFromZip());
        }

        /// <summary>
        /// This event is used to navigate to contact email screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEmail_Click(object sender, EventArgs e)
        {
            if (!IsValidate()) return;
            _dealerNetworkModel = new DealerNetworkModel();
            _dealerNetworkModel.CountryCode = cmbCountryCode.SelectedValue.ToString();
            _dealerNetworkModel.MakeCode = cmbMake.SelectedValue.ToString();
            _dealerNetworkModel.DealerId = txtDealerId.Text;
            Task.FromResult(GetContactEmail());
            var contactEmail = ActivatorUtilities.CreateInstance(ServiceLocator.InstanceProvider, typeof(ContactEmailForm), _dealerNetworkModel, _contactEmailModel);
            ((Form)contactEmail).ShowDialog();
        }

        /// <summary>
        /// This event is used to clear details of get ready when unchecked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Check_CheckedChanged(object sender, System.EventArgs e)
        {
            if (chkCars.Checked || chkLocal.Checked || chkTrucks.Checked || chkLuxury.Checked)
            {
                ViewGetReadyDetails();
            }

        }
        /// <summary>
        /// To load basic requirements
        /// </summary>
        private void DealerOnLoad()
        {
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            Task.FromResult(GetCountryDetail());
            Task.FromResult(GetMakeDetail());
            PrintCourtesyList();
            TermOverrideList();
            ShowStatusBarMessage(DealerConstant.AddingDealer);
            Text = DealerConstant.DealerNetworkTitle;
            gbGetReady.Enabled = false;
        }

        /// <summary>
        /// Add dealer
        /// </summary>
        private async Task InsertDealer()
        {
            if (!IsValidate()) return;
            ShowStatusBarMessage(DealerConstant.InsertingRecord);
            LogInformation(DealerConstant.ExecutingInsert);
            if (string.IsNullOrEmpty(cmbPrintNotice.Text) || (cmbTerms.Enabled && string.IsNullOrEmpty(cmbTerms.Text)))
            {
                ShowValidationMessage(DealerConstant.NotValidEntry);
            }
            else
            {
                MapDealerDetail();
                var insertData = await _dealerNetwork.InsertDealerNetwork(_dealerNetworkModel);
                if (insertData.IsSuccess)
                {
                    ResetDealer();
                    ShowStatusBarMessage(DealerConstant.InsertedRecord);
                    LogInformation(DealerConstant.ExecutedInsert);
                }
                else
                {
                    ShowStatusBarMessage(DealerConstant.InsertedFailRecord);
                    LogWarning(DealerConstant.FailedInsert);
                }
            }
        }

        /// <summary>
        /// Update dealer details
        /// </summary>
        private async Task UpdateDealer()
        {
            if (!IsValidate()) return;
            ShowStatusBarMessage(DealerConstant.UpdatingRecord);
            LogInformation(DealerConstant.ExecutingUpdate);
            if (string.IsNullOrEmpty(cmbPrintNotice.Text) || (cmbTerms.Enabled && string.IsNullOrEmpty(cmbTerms.Text)))
            {
                ShowValidationMessage(DealerConstant.NotValidEntry);
            }
            else
            {
                MapDealerDetail();
                var updateData = await _dealerNetwork.UpdateDealerNetwork(_dealerNetworkModel);
                if (updateData.IsSuccess)
                {
                    ResetDealer();
                    ShowStatusBarMessage(DealerConstant.UpdatedRecord);
                    LogInformation(DealerConstant.ExecutedUpdate);
                }
                else
                {
                    ShowStatusBarMessage(DealerConstant.UpdatedDealerRecordFailed);
                    LogWarning(DealerConstant.FailedUpdate);
                }
            }
        }

        /// <summary>
        /// This method is used to delete dealer, passing through the dealer command
        /// </summary>
        private async Task DeleteDealer()
        {
            ShowStatusBarMessage();
            if (!IsValidate()) return;
            var messageBoxResult = _message.Show(BaseConstant.DeleteConfirmation,
               BaseConstant.DeleteConfirmationTitle, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                ShowStatusBarMessage(DealerConstant.DeletingDealer);
                LogInformation(DealerConstant.DeletingDealer);
                MapDealerDetail();
                var deleteData = await _dealerNetwork.DeleteDealerNetwork(_dealerNetworkModel);

                if (deleteData.IsSuccess)
                {
                    ResetDealer();
                    ShowStatusBarMessage(DealerConstant.DeletedDealer);
                    LogInformation(DealerConstant.DeletingDealer);
                }
                else
                {
                    ShowStatusBarMessage();
                    _message.Show(DealerConstant.DeleteDealerFail, BaseConstant.ErrorMessageBoxTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                    LogWarning(DealerConstant.DeleteDealerFail);
                }
            }
        }

        /// <summary>
        /// Get City,State from zip code
        /// </summary>
        private async Task GetCityFromZip()
        {
            var result = await _dealerNetwork.GetCityFromZip(txtZipCode.Text.Trim());
            if (!result.HasValue)
            {
                ShowValidationMessage(DealerConstant.ZipCode);
                txtCity.Text = string.Empty;
                txtState.Text = string.Empty;
            }
            else
            {
                txtCity.Text = result.Value.City;
                txtState.Text = result.Value.State;
            }
        }

        /// <summary>
        /// Get contact email details
        /// </summary>
        private async Task GetContactEmail()
        {
            _contactEmailModel = new ContactEmailModel();
            var contactDetails = await _dealerNetwork.GetContactEmailDetail(_dealerNetworkModel.CountryCode, _dealerNetworkModel.MakeCode, _dealerNetworkModel.DealerId);
            _contactEmailModel.StockContactName = contactDetails.Value
                .Where(x => x.ContactType == ContactType.Stock.ToString()).Select(x => x.ContactName).FirstOrDefault();
            _contactEmailModel.StockContactEmail = contactDetails.Value
                .Where(x => x.ContactType == ContactType.Stock.ToString()).Select(x => x.ContactEmail).FirstOrDefault();
            _contactEmailModel.FinanceContactName = contactDetails.Value
                .Where(x => x.ContactType == ContactType.Finance.ToString()).Select(x => x.ContactName).FirstOrDefault();
            _contactEmailModel.FinanceContactEmail = contactDetails.Value
                .Where(x => x.ContactType == ContactType.Finance.ToString()).Select(x => x.ContactEmail).FirstOrDefault();
            _contactEmailModel.LicenseContactName = contactDetails.Value
                .Where(x => x.ContactType == ContactType.Licence.ToString()).Select(x => x.ContactName).FirstOrDefault();
            _contactEmailModel.LicenseContactEmail = contactDetails.Value
                .Where(x => x.ContactType == ContactType.Licence.ToString()).Select(x => x.ContactEmail).FirstOrDefault();
            _dealerNetworkModel.ContactEmail = _contactEmailModel;
        }

        /// <summary>
        /// Get Getready Details
        /// </summary>
        private async Task GetReadyDetails()
        {
            SetCompositeKey();
            var getReadyList = await _dealerNetwork.GetReadyDetails(_getReadyModel);
            dgReady.DataSource = getReadyList.Value;

            if (dgReady.Rows.Count == 0)
            {
                dgReady.Enabled = false;
                dgReady.ColumnHeadersVisible = false;
                dgReady.DataSource = null;
            }
            else
            {
                dgReady.ColumnHeadersDefaultCellStyle.BackColor = this.BackColor;
                dgReady.EnableHeadersVisualStyles = false;
                dgReady.Columns["CountryCode"].Visible = false;
                dgReady.Columns["MakeCode"].Visible = false;
                dgReady.Columns["DealerId"].Visible = false;
                dgReady.Columns["GetReadyCategory"].HeaderText = "Category";
                dgReady.Columns["ClientId"].HeaderText = "Client";
                dgReady.Columns["GetReadyAmount"].HeaderText = "Amount";
                dgReady.Columns["GetReadyEffectiveDate"].HeaderText = "Eff Date";
                dgReady.Columns["GetReadyEffectiveDate"].DefaultCellStyle.Format = BaseConstant.DateFormat;
                dgReady.Columns["LastProgram"].HeaderText = "Last Prgm";
                dgReady.Columns["LastUser"].HeaderText = "Last User";
                dgReady.Columns["LastChange"].HeaderText = "Last Chg";
                dgReady.Columns["LastChange"].DefaultCellStyle.Format = BaseConstant.DateFormat;
                dgReady.Enabled = true;
                dgReady.ReadOnly = true;
                dgReady.ColumnHeadersVisible = true;
                dgReady.RowHeadersVisible = false;
            }
        }

        /// <summary>
        /// Set insert get ready process
        /// </summary>
        /// <returns>Task</returns>
        private async Task SetInsertGetReadyProcess()
        {
            try
            {
                var insertData = await _dealerNetwork.InsertGetReady(_getReadyModel);

                if (insertData.IsSuccess)
                {
                    ShowStatusBarMessage(DealerConstant.InsertedGetReadyRecord);
                    LogInformation(DealerConstant.ExecutedGetReadyInsert);
                    _getReadyModel.GetReadyCategories = new List<string>();
                    ResetGetReady();
                    await GetReadyDetails();
                }
                else
                {
                    LogWarning(DealerConstant.FailedGetReadyInsert);
                }
            }
            catch
            {
                ResetGetReady();
                await GetReadyDetails();
                throw;
            }
        }

        /// <summary>
        /// Set the update get ready
        /// </summary>
        /// <returns></returns>
        private async Task SetUpdateGetReady()
        {
            try
            {
                var updateData = await _dealerNetwork.UpdateGetReady(_getReadyModel);
                if (updateData.IsSuccess)
                {
                    ShowStatusBarMessage(DealerConstant.UpdatedGetReadyRecord);
                    LogInformation(DealerConstant.ExecutedGetReadyUpdate);
                    _getReadyModel.GetReadyCategories = new List<string>();
                    ResetGetReady();
                    await GetReadyDetails();
                }
                else
                {
                    LogWarning(DealerConstant.FailedGetReadyUpdate);
                }
            }
            catch
            {
                ResetGetReady();
                await GetReadyDetails();
                dgReady.Enabled = true;
                _isGREdit = true;
                throw;
            }
        }

        /// <summary>
        /// set the delete get ready process
        /// </summary>
        /// <returns></returns>
        private async Task SetDeleteGetReadyProcess()
        {
            try
            {
                var deleteData = await _dealerNetwork.DeleteGetReady(_getReadyModel);
                if (deleteData.IsSuccess)
                {
                    ShowStatusBarMessage(DealerConstant.DeletedGetReadyRecord);
                    LogInformation(DealerConstant.ExecutedGetReadyDelete);
                    ResetGetReady();
                    await GetReadyDetails();
                }
                else
                {
                    LogWarning(DealerConstant.FailedGetReadyDelete);
                }
            }
            catch
            {
                ResetGetReady();
                await GetReadyDetails();
                dgReady.Enabled = true;
                _isGREdit = true;
                throw;
            }
        }

        /// <summary>
        /// Show Get Rady Details on UI
        /// </summary>
        private void ViewGetReadyDetails()
        {
            if (_isGREdit)
            {
                Task.FromResult(GetReadyDetails());
                _isGREdit = false;
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
        private async Task GetMakeDetail()
        {
            var makeData = await _masterData.GetMakeDetail();
            _makes.AddRange(makeData.Value);
            makes = new List<MakeModel>(_makes);
        }

        /// <summary>
        /// Country selection change to fill make code
        /// </summary>
        private void CountryChanged()
        {
            CountryCodeBaseControlEnabled();
            BindMakeByCountry(makes, cmbCountryCode, cmbMake);
        }

        /// <summary>
        /// Set Print Courtesy List
        /// </summary>
        /// <returns></returns>
        private void PrintCourtesyList()
        {
            cmbPrintNotice.Items.Clear();
            cmbPrintNotice.Items.AddRange(new object[] {
            "",
            "Fax",
            "Email"});
        }

        /// <summary>
        /// Set Term Override List
        /// </summary>
        /// <returns></returns>
        private void TermOverrideList()
        {
            cmbTerms.Items.Clear();
            cmbTerms.Items.AddRange(new object[] {
            "",
            "Due01",
            "Due07",
            "Due10",
            "Due15",
            "Due20",
            "Due25",
            "Due30",
            "Rcv25",
            "Rec45",
            "10N1P"});
        }

        /// <summary>
        /// Enable/Disable controls based on conuntry code and user department
        /// </summary>
        private void CountryCodeBaseControlEnabled()
        {
            string _userId = "PPAGE";
            string _userDept = "MIS";
            string[] _userIdList = { "PPAGE", "BROSE", "CARORK", "TAUGHER", "RTRKULJA", "JDEWEY", "CFOSTER", "RSKVARLA", "CRICHEAL", "CHORN" };
            string[] _userSecurityList = { "0710000006", "0710000008", "0710000009", "0710000010", "0710000000" };
            string[] _userIdSecurityList = { "KUNKEL", "DFARQUHA", "JKULIK" };
            string[] _userIdUS = { "JDEWEY", "CFOSTER", "RSKVARLA", "CRICHEAL", "CHORN" };

            IsActiveFactry(false);
            IsActiveKd(false);
            IsActiveVolume(false);
            IsActiveBank(false);
            IsActiveTermsOverride(false);

            if (!string.IsNullOrEmpty(cmbCountryCode.SelectedValue.ToString()) && (cmbCountryCode.SelectedValue.ToString() == "UK" || cmbCountryCode.SelectedValue.ToString() == "US" ||
                _userIdList.Contains(_userId.ToUpper().Trim()) || _userDept.ToUpper().Trim() == "MIS"))
            {
                IsActiveTermsOverride(true);
                IsActiveBank(true);

                if (cmbCountryCode.SelectedValue.ToString() == "US")
                {
                    var userSecurityId = GetUserSecurityCode(_userId.ToUpper().Trim());
                    if (!_userSecurityList.Contains(userSecurityId) && _userIdSecurityList.Contains(_userId.ToUpper().Trim()))
                    {
                        IsActiveBank(false);
                    }

                    if (_userIdUS.Contains(_userId.ToUpper().Trim()) || _userDept.ToUpper().Trim() == "MIS")
                    {
                        IsActiveKd(true);
                    }
                }
                else
                {
                    IsActiveVolume(true);
                }
            }
            if (cmbCountryCode.SelectedValue.ToString() == "CA")
            {
                IsActiveFactry(true);
            }
            ResetDisabledProperties();
        }

        /// <summary>
        /// Get User Security Code from data source
        /// </summary>
        private string GetUserSecurityCode(string userId)
        {
            var result = _dealerNetwork.GetUserSecurityCode(userId);
            return result.Result.ToString();
        }

        /// <summary>
        /// Enable/Disable Terms Override details
        /// </summary>
        /// <param name="isEnable"></param>
        private void IsActiveTermsOverride(bool isEnable)
        {
            cmbTerms.Enabled = isEnable;
        }

        /// <summary>
        /// Enable/Disable bank details
        /// </summary>
        /// <param name="isEnable"></param>
        private void IsActiveBank(bool isEnable)
        {
            txtBankNo1.Enabled = isEnable;
            txtBankAcct.Enabled = isEnable;
            txtBankName.Enabled = isEnable;
            txtBankCity.Enabled = isEnable;
        }

        /// <summary>
        /// Enable/Disable volume rebates details
        /// </summary>
        /// <param name="isEnable"></param>
        private void IsActiveVolume(bool isEnable)
        {
            txtFactory.Enabled = isEnable;
            txtStock1.Enabled = isEnable;
        }

        /// <summary>
        /// Enable/Disable key dealer details
        /// </summary>
        /// <param name="isEnable"></param>
        private void IsActiveKd(bool isEnable)
        {
            txtClient1.Enabled = isEnable;
            txtDiv.Enabled = isEnable;
        }

        /// <summary>
        /// Enable/Disable Factory (Get Ready )
        /// </summary>
        /// <param name="isEnable"></param>
        private void IsActiveFactry(bool isEnable)
        {
            chkFactry.Enabled = isEnable;
        }

        /// <summary>
        ///  Map dealer details to Dealer network model
        /// </summary>
        private void MapDealerDetail()
        {
            _dealerNetworkModel = new DealerNetworkModel();
            DealerInfo();
            DealerContactInfo();
            DealerAddressInfo();
            DelaerProgramInfo();
            DealerOtherInfo();
        }

        /// <summary>
        /// Set Dealer information to dealer model
        /// </summary>
        private void DealerInfo()
        {
            _dealerNetworkModel.CountryCode = cmbCountryCode.SelectedValue.ToString();
            _dealerNetworkModel.DealerId = txtDealerId.Text.Trim();
            _dealerNetworkModel.MakeCode = cmbMake.SelectedValue.ToString();
            _dealerNetworkModel.VendorId = txtVendorId.Text.Trim();
            _dealerNetworkModel.VendorName = txtName.Text.Trim();
        }

        /// <summary>
        /// Set program information to dealer model
        /// </summary>
        private void DelaerProgramInfo()
        {
            _dealerNetworkModel.CreationPrgm = txtProgram.Text.Trim();
            _dealerNetworkModel.CreationUser = txtUser.Text.Trim();
            _dealerNetworkModel.CreationDate = GetValidDate(txtCreationDate.Text.Trim());
            _dealerNetworkModel.LastUsedDate = GetValidDate(txtLastUsed.Text.Trim());
            _dealerNetworkModel.LastPrgm = txtLastProgram.Text.Trim();
            _dealerNetworkModel.LastUser = txtLastUser.Text.Trim();
            _dealerNetworkModel.LastChg = GetValidDate(txtLastChg.Text.Trim());
        }

        /// <summary>
        /// Set Dealer address information to dealer model
        /// </summary>
        private void DealerAddressInfo()
        {
            _dealerNetworkModel.Address1 = txtAddress1.Text.Trim();
            _dealerNetworkModel.Address2 = txtAddress2.Text.Trim();
            _dealerNetworkModel.Address3 = txtAddress3.Text.Trim();
            _dealerNetworkModel.Address4 = txtAddress4.Text.Trim();
            _dealerNetworkModel.City = txtCity.Text.Trim();
            _dealerNetworkModel.State = txtState.Text.Trim();
            _dealerNetworkModel.ZipCode = txtZipCode.Text.Trim();
            _dealerNetworkModel.ZipPlus4 = txtZipCity.Text.Trim();
        }

        /// <summary>
        /// Set Dealer contact information to dealer model
        /// </summary>
        private void DealerContactInfo()
        {
            _dealerNetworkModel.Contact1 = txtContact1.Text.Trim();
            _dealerNetworkModel.Contact2 = txtContact2.Text.Trim();
            _dealerNetworkModel.Phone1Exchange = txtPhonec1.Text.Trim();
            _dealerNetworkModel.Phone1Number = txtPhone1.Text.Trim();
            _dealerNetworkModel.Phone2Exchange = txtPhonec2.Text.Trim();
            _dealerNetworkModel.Phone2Number = txtPhone2.Text.Trim();
            _dealerNetworkModel.FaxNumber = txtFax.Text.Trim();
            _dealerNetworkModel.Email = txtEmail.Text.Trim();
        }

        /// <summary>
        /// Set Dealer other information to dealer model
        /// </summary>
        private void DealerOtherInfo()
        {
            int? _nullNumber = null;
            float? _nullDouble = null;

            _dealerNetworkModel.GmBusnAsctCd = txtBacCode.Text.Trim();
            _dealerNetworkModel.SellingDelivInd = txtSelling.Text.Trim();
            _dealerNetworkModel.MinorityInd = txtMinority.Text.Trim();
            _dealerNetworkModel.DealerRating = txtDealer.Text.Trim();
            _dealerNetworkModel.MfgZoneCode = txtMfg.Text.Trim();
            _dealerNetworkModel.PrintDealerDraft = txtDraft.Text.Trim();
            _dealerNetworkModel.DraftAcct1 = txtDraftAcc1.Text.Trim();
            _dealerNetworkModel.DraftAcct2 = txtDraftAcc.Text.Trim();
            _dealerNetworkModel.TaxId = txtTaxId.Text.Trim();
            _dealerNetworkModel.TermsOverride = cmbTerms.Text.Trim();
            _dealerNetworkModel.PayToVendor = txtPayToVendor.Text.Trim();
            _dealerNetworkModel.FactoryCountPrior12 = txtFactoryCount.Text.Trim() != "" ? int.Parse(txtFactoryCount.Text.Trim()) : _nullNumber;
            _dealerNetworkModel.StockCountPrior12 = txtStock.Text.Trim() != "" ? int.Parse(txtStock.Text.Trim()) : _nullNumber;
            _dealerNetworkModel.PymtVia = txtPaymentVia.Text.Trim();
            _dealerNetworkModel.CourtesyDelivPrintInd = cmbPrintNotice.Text.Trim();
            _dealerNetworkModel.CertInd = txtCertificate.Text.Trim();
            _dealerNetworkModel.BankNumber = txtBankNo1.Text.Trim();
            _dealerNetworkModel.BankName = txtBankName.Text.Trim();
            _dealerNetworkModel.BankCity = txtBankCity.Text.Trim();
            _dealerNetworkModel.BankAccount = txtBankAcct.Text.Trim();
            _dealerNetworkModel.UkCommission = txtCommission.Text.Trim() != "" ? int.Parse(txtCommission.Text.Trim()) : _nullDouble;
            _dealerNetworkModel.DealerComments = txtDealerComment.Text.Trim();
            _dealerNetworkModel.KdClient = txtClient1.Text.Trim();
            _dealerNetworkModel.KdDiv = txtDiv.Text.Trim();
            _dealerNetworkModel.CanVolumeFactory = txtFactory.Text.Trim() != "" ? int.Parse(txtFactory.Text.Trim()) : _nullNumber;
            _dealerNetworkModel.CanVolumeStock = txtStock1.Text.Trim() != "" ? int.Parse(txtStock1.Text.Trim()) : _nullNumber;
        }

        /// <summary>
        ///  Get date in Valid format
        /// </summary>
        /// <param name="date"></param>
        private DateTime? GetValidDate(string date)
        {
            if (!string.IsNullOrEmpty(date))
            {
                return DateTime.ParseExact(date, BaseConstant.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);
            }
            return null;
        }

        /// <summary>
        ///  Bind selected row details of Get Ready 
        /// </summary>
        /// <param name="getReadyListModel"></param>
        private void BindGetReadyDetail(GetReadyModel getReadyListModel)
        {
            DisableGetVehicles();
            if (dgReady.CurrentRow.Cells["GetReadyEffectiveDate"].Value != null)
            {
                txtEffectiveDate.Text = DateTime.Parse(dgReady.CurrentRow.Cells["GetReadyEffectiveDate"].Value.ToString()).ToString(BaseConstant.DateFormat);
            }
            else
            {
                txtEffectiveDate.Text = string.Empty;
            }
            CheckGetReadyCategory(getReadyListModel);
        }

        /// <summary>
        /// Check get ready category
        /// </summary>
        /// <param name="getReadyListModel"></param>
        private void CheckGetReadyCategory(GetReadyModel getReadyListModel)
        {
            switch (getReadyListModel.GetReadyCategory)
            {
                case "Cars":
                    chkCars.Checked = true;
                    break;
                case "Trucks":
                    chkTrucks.Checked = true;
                    break;
                case "Local":
                    chkLocal.Checked = true;
                    break;
                case "Luxury":
                    chkLuxury.Checked = true;
                    break;
                case "Factry":
                    chkFactry.Checked = true;
                    break;
                case "Stocks":
                    chkStocks.Checked = true;
                    break;
            }
        }

        /// <summary>
        /// Validate the get ready fields
        /// </summary>
        /// <returns>bool</returns>
        private bool IsValidateGetReady()
        {
            if (!IsValidate())
            {
                return false;
            }

            if (!ValidateEffectiveDate())
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// This event is used to validate the effective date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private bool ValidateEffectiveDate()
        {
            if (!IsEmptyDateValue(txtEffectiveDate.Text, txtEffectiveDate.Mask))
            {
                DateTime _date;
                var date = DateTime.TryParseExact(txtEffectiveDate.Text, BaseConstant.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _date);
                if (!date)
                {
                    ShowValidationMessage(BaseConstant.EffectDateValidateErrorMessage);
                    txtEffectiveDate.Focus();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Set other details of Get Ready
        /// </summary>
        private void SetGetReadyDetails()
        {
            List<string> vehicles = new List<string>();
            if (chkCars.Checked)
            {
                vehicles.Add("Cars");
                _getReadyModel.GetReadyCategory = "Cars";
            }
            if (chkTrucks.Checked)
            {
                vehicles.Add("Trucks");
                _getReadyModel.GetReadyCategory = "Trucks";
            }
            if (chkLocal.Checked)
            {
                vehicles.Add("Local");
                _getReadyModel.GetReadyCategory = "Local";
            }
            if (chkLuxury.Checked)
            {
                vehicles.Add("Luxury");
                _getReadyModel.GetReadyCategory = "Luxury";
            }
            if (chkFactry.Checked)
            {
                vehicles.Add("Factry");
                _getReadyModel.GetReadyCategory = "Factry";
            }
            if (chkStocks.Checked)
            {
                vehicles.Add("Stocks");
                _getReadyModel.GetReadyCategory = "Stocks";
            }
            if (vehicles.Count > 0)
            {
                _getReadyModel.GetReadyCategories = vehicles;
            }
            if (string.IsNullOrEmpty(txtClient.Text) || txtClient.Text == " ")
            {
                _getReadyModel.ClientId = "*";
            }
            else
            {
                _getReadyModel.ClientId = txtClient.Text.Trim();
            }
            if (string.IsNullOrEmpty(txtAmount.Text) || txtAmount.Text == " ")
            {
                _getReadyModel.GetReadyAmount = 0;
            }
            else
            {
                _getReadyModel.GetReadyAmount = Convert.ToDecimal(txtAmount.Text);
            }
        }

        /// <summary>
        /// Set the effective date
        /// </summary>
        private void SetEffectiveDate()
        {
            if (IsEmptyDateValue(txtEffectiveDate.Text, txtEffectiveDate.Mask))
            {
                txtEffectiveDate.Text = DateTime.Now.Date.ToString(BaseConstant.DateFormat);
                _getReadyModel.GetReadyEffectiveDate = DateTime.ParseExact(txtEffectiveDate.Text, BaseConstant.DateFormat, CultureInfo.InvariantCulture);
            }
            else
            {
                _getReadyModel.GetReadyEffectiveDate = DateTime.ParseExact(txtEffectiveDate.Text, BaseConstant.DateFormat, CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        ///  Set composite key for get ready
        /// </summary>
        private void SetCompositeKey()
        {
            _getReadyModel = new GetReadyModel();
            _getReadyModel.CountryCode = cmbCountryCode.SelectedValue.ToString();
            _getReadyModel.MakeCode = cmbMake.SelectedValue.ToString();
            _getReadyModel.DealerId = txtDealerId.Text.Trim();
        }

        /// <summary>
        /// To check UI validation 
        /// </summary> 
        private bool IsValidate()
        {
            if (CheckEmptyProperty(cmbCountryCode))
            {
                ShowValidationMessage(DealerConstant.CountryCodeRequired);
                return false;
            }
            if (CheckEmptyProperty(cmbMake))
            {
                ShowValidationMessage(DealerConstant.MakeCodeRequired);
                return false;
            }
            if (CheckEmptyProperty(txtDealerId))
            {
                ShowValidationMessage(DealerConstant.DealerIdRequired);
                return false;
            }
            return true;
        }

        /// <summary>
        ///  Reset Value of disabled dealer's details
        /// </summary>
        private void ResetDisabledProperties()
        {
            if (!cmbTerms.Enabled)
            {
                cmbTerms.Text = string.Empty;
            }
            ResetDisabledBankDetail();
            ResetDisabledKeyDealerDetail();
            ResetDisabledVolumeRebate();
        }

        /// <summary>
        /// Reset Disabled Key Dealer Detail
        /// </summary>
        private void ResetDisabledKeyDealerDetail()
        {
            if (!txtClient1.Enabled)
            {
                txtClient1.Text = string.Empty;
            }
            if (!txtDiv.Enabled)
            {
                txtDiv.Text = string.Empty;
            }
        }

        /// <summary>
        /// Reset Disabled Volume Rebate
        /// </summary>
        private void ResetDisabledVolumeRebate()
        {
            if (!txtFactory.Enabled)
            {
                txtFactory.Text = string.Empty;
            }
            if (!txtStock1.Enabled)
            {
                txtStock1.Text = string.Empty;
            }
        }

        /// <summary>
        /// Reset Disabled Bank Detail
        /// </summary>
        private void ResetDisabledBankDetail()
        {
            if (!txtBankNo1.Enabled)
            {
                txtBankNo1.Text = string.Empty;
            }
            if (!txtBankAcct.Enabled)
            {
                txtBankAcct.Text = string.Empty;
            }
            if (!txtBankName.Enabled)
            {
                txtBankName.Text = string.Empty;
            }
            if (!txtBankCity.Enabled)
            {
                txtBankCity.Text = string.Empty;
            }
        }

        /// <summary>
        /// Reset dealer
        /// </summary>
        private void ResetDealer()
        {
            makes = new List<MakeModel>();
            ResetProperty();
            DisableGetVehicles();
            ResetDealerButton();
            ClearFormDetails(panelDealer);
            cmbCountryCode.SelectedIndex = 0;
            cmbMake.SelectedIndex = 0;
            cmbPrintNotice.SelectedIndex = 0;
            cmbTerms.SelectedIndex = 0;
            dgReady.DataSource = null;
            gbGetReady.Enabled = false;
        }

        /// <summary>
        /// Reset properties of dealer
        /// </summary>
        private void ResetProperty()
        {
            _dealerNetworkModel = new DealerNetworkModel();
        }

        /// <summary>
        /// Reset Dealer's Buttons
        /// </summary>
        private void ResetDealerButton()
        {
            btnCopyDealer.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnCancel.Enabled = true;
        }

        /// <summary>
        ///  Disable Checkbox for Gr Vehicles
        /// </summary>
        private void DisableGetVehicles()
        {
            chkCars.Checked = false;
            chkTrucks.Checked = false;
            chkLocal.Checked = false;
            chkFactry.Checked = false;
            chkStocks.Checked = false;
            chkLuxury.Checked = false;
        }

        /// <summary>
        ///  Reset Get Ready contains
        /// </summary>
        private void ResetGetReady()
        {
            dgReady.Enabled = false;
            DisableGetVehicles();
            ClearGerReadyDetail();
        }
        /// <summary>
        /// Clear Get Ready Details
        /// </summary>
        private void ClearGerReadyDetail()
        {
            txtClient.Text = string.Empty;
            txtEffectiveDate.Text = null;
            txtAmount.Text = null;
            txtEffectiveDate.Text = string.Empty;
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

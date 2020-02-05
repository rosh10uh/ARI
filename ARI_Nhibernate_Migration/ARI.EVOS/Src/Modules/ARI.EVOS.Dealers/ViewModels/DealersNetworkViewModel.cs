using ARI.EVOS.Common.Models;
using ARI.EVOS.Dealer.AppServices.Interface;
using ARI.EVOS.Dealer.Domain.SharedKernel;
using ARI.EVOS.Dealers.Constant;
using ARI.EVOS.Dealers.Models;
using ARI.EVOS.Dealers.Views;
using ARI.EVOS.Infra;
using ARI.EVOS.Infra.ViewModel;
using Microsoft.Extensions.Logging;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ARI.EVOS.Dealers.ViewModels
{
    /// <summary>
    ///  This class is used to map with Dealer UI Screen (View Model)
    /// </summary>
    public class DealersNetworkViewModel : BaseViewModel, INavigationAware
    {
        private readonly IContainerProvider _containerProvider;
        private readonly IRegionManager _regionManager;
        private readonly IDealerNetwork _dealerNetwork;
        private readonly IMasterData _masterData;

        private GetReadyModel _getReadyModel;
        private DealerNetworkModel _dealerNetworkModel;
        private ContactEmailModel _contactEmailModel;
        private CountryModel _countryModel;
        private MakeModel _makeModel;
        private PrintCourtesyModel _printCourtesy;
        private TermsOverrideModel _termsOverride;
        private DealerSearchModel _selectedDealer;
        private string _selectedCountry = string.Empty;
        private string _selectedMake = string.Empty;
        private string _selectedPrintCourtesy = string.Empty;
        private string _selectedTermOverride = string.Empty;
        private string _visiblityHeader = "None";
        private string _effectiveDate = string.Empty;
        private bool _isActiveVolume = true;
        private bool _isActiveKd = true;
        private bool _isActiveFactry = true;
        private bool _isActiveBank = true;
        private bool _isActiveTermsOverride = true;
        private bool _isCheckedCar = false;
        private bool _isCheckedTrucks = false;
        private bool _isCheckedLocal = false;
        private bool _isCheckedLuxury = false;
        private bool _isCheckedFactry = false;
        private bool _isCheckedStocks = false;
        private bool _isGREdit = false;
        private bool _isActiveGetReady = false;
        private bool _isActiveGetReadyList = true;
        private bool _isEnableCopyDealerButton = true;
        private bool _isEnableUpdateDealerButton = true;
        private bool _isEnableDeleteButton = true;
        private bool _isEnableCancelButton = true;
        private readonly List<MakeModel> _makes = new List<MakeModel>();
        private readonly List<PrintCourtesyModel> _printCourtesies;
        private readonly List<TermsOverrideModel> _termsOverrides;
        private ObservableCollection<GetReadyModel> _getReadies = new ObservableCollection<GetReadyModel>();

        public string EffectiveDate
        {
            get => _effectiveDate;
            set
            {
                _effectiveDate = value;
                RaisePropertyChanged("EffectiveDate");
            }
        }
        public DealerSearchModel SelectedDealer
        {
            get => _selectedDealer;
            set => SetProperty(ref _selectedDealer, value);
        }

        public bool IsEnableCopyDealerButton
        {
            get { return _isEnableCopyDealerButton; }
            set
            {
                _isEnableCopyDealerButton = value;
                RaisePropertyChanged("IsEnableCopyDealerButton");
            }
        }
        public bool IsEnableUpdateDealerButton
        {
            get { return _isEnableUpdateDealerButton; }
            set
            {
                _isEnableUpdateDealerButton = value;
                RaisePropertyChanged("IsEnableUpdateDealerButton");
            }
        }
        public bool IsEnableDeleteButton
        {
            get { return _isEnableDeleteButton; }
            set
            {
                _isEnableDeleteButton = value;
                RaisePropertyChanged("IsEnableDeleteButton");
            }
        }
        public bool IsEnableCancelButton
        {
            get { return _isEnableCancelButton; }
            set
            {
                _isEnableCancelButton = value;
                RaisePropertyChanged("IsEnableCancelButton");
            }
        }
        public bool IsActiveTermsOverride
        {
            get => _isActiveTermsOverride;
            set
            {
                _isActiveTermsOverride = value;
                RaisePropertyChanged("IsActiveTermsOverride");
            }
        }
        public string VisiblityHeader
        {
            get => _visiblityHeader;
            set
            {
                _visiblityHeader = value;
                RaisePropertyChanged("VisiblityHeader");
            }
        }

        public bool IsEnableGetReadyList
        {
            get => _isActiveGetReadyList;
            set
            {
                _isActiveGetReadyList = value;
                RaisePropertyChanged("IsEnableGetReadyList");
            }
        }

        public bool IsEnableGetReady
        {
            get => _isActiveGetReady;
            set
            {
                _isActiveGetReady = value;
                RaisePropertyChanged("IsEnableGetReady");
            }
        }

        public bool IsActiveBank
        {
            get => _isActiveBank;
            set
            {
                _isActiveBank = value;
                RaisePropertyChanged("IsActiveBank");
            }
        }

        public bool IsActiveVolume
        {
            get => _isActiveVolume;
            set
            {
                _isActiveVolume = value;
                RaisePropertyChanged("IsActiveVolume");
            }
        }
        public bool IsActiveFactry
        {
            get => _isActiveFactry;
            set
            {
                _isActiveFactry = value;
                RaisePropertyChanged("IsActiveFactry");
            }
        }

        public bool IsActiveKd
        {
            get => _isActiveKd;
            set
            {
                _isActiveKd = value;
                RaisePropertyChanged("IsActiveKd");
            }
        }
        public bool IsCheckedCars
        {
            get => _isCheckedCar;
            set
            {
                _isCheckedCar = value;
                RaisePropertyChanged("IsCheckedCars");
            }
        }

        public bool IsCheckedTrucks
        {
            get => _isCheckedTrucks;
            set
            {
                _isCheckedTrucks = value;
                RaisePropertyChanged("IsCheckedTrucks");
            }
        }

        public bool IsCheckedLocal
        {
            get => _isCheckedLocal;
            set
            {
                _isCheckedLocal = value;
                RaisePropertyChanged("IsCheckedLocal");
            }
        }

        public bool IsCheckedLuxury
        {
            get => _isCheckedLuxury;
            set
            {
                _isCheckedLuxury = value;
                RaisePropertyChanged("IsCheckedLuxury");
            }
        }

        public bool IsCheckedFactry
        {
            get => _isCheckedFactry;
            set
            {
                _isCheckedFactry = value;
                RaisePropertyChanged("IsCheckedFactry");
            }
        }

        public bool IsCheckedStocks
        {
            get => _isCheckedStocks;
            set
            {
                _isCheckedStocks = value;
                RaisePropertyChanged("IsCheckedStocks");
            }
        }

        public DealerNetworkModel DealerNetwork
        {
            get => _dealerNetworkModel;
            set => SetProperty(ref _dealerNetworkModel, value);
        }

        public ContactEmailModel ContactEmail
        {
            get => _contactEmailModel;
            set => SetProperty(ref _contactEmailModel, value);
        }
        public CountryModel Country
        {
            get => _countryModel;
            set
            {
                SetProperty(ref _countryModel, value);
                DealerNetwork.CountryCode = _countryModel?.CountryCode;
            }
        }
        public string SelectedCountry
        {
            get => _selectedCountry;
            set
            {
                _selectedCountry = value;
                RaisePropertyChanged("SelectedCountry");
            }
        }
        public string SelectedMake
        {
            get => _selectedMake;

            set
            {
                _selectedMake = value;
                RaisePropertyChanged("SelectedMake");
            }
        }

        public string SelectedPrintCourtesy
        {
            get => _selectedPrintCourtesy;

            set
            {
                _selectedPrintCourtesy = value;
                RaisePropertyChanged("SelectedPrintCourtesy");
            }
        }

        public string SelectedTermOverride
        {
            get => _selectedTermOverride;

            set
            {
                _selectedTermOverride = value;
                RaisePropertyChanged("SelectedTermOverride");
            }
        }
        public MakeModel Make
        {
            get => _makeModel;
            set
            {
                SetProperty(ref _makeModel, value);
                DealerNetwork.MakeCode = _makeModel?.MakeCode;
            }
        }

        public PrintCourtesyModel PrintCourtesy
        {
            get => _printCourtesy;
            set
            {
                SetProperty(ref _printCourtesy, value);
                DealerNetwork.CourtesyDelivPrintInd = _printCourtesy?.PrintCourtesy;
            }
        }

        public TermsOverrideModel TermsOverride
        {
            get => _termsOverride;
            set
            {
                SetProperty(ref _termsOverride, value);
                DealerNetwork.TermsOverride = _termsOverride?.TermsOverride;
            }
        }

        public GetReadyModel GetReadyModel
        {
            get => _getReadyModel;
            set => SetProperty(ref _getReadyModel, value);
        }

        public ObservableCollection<GetReadyModel> GetReadies
        {
            get => _getReadies;
            set => SetProperty(ref _getReadies, value);
        }

        public ObservableCollection<CountryModel> Countries { get; set; }
        public ObservableCollection<MakeModel> Makes { get; set; }
        public ObservableCollection<PrintCourtesyModel> PrintCourtesies { get; set; }
        public ObservableCollection<TermsOverrideModel> TermsOverrides { get; set; }
        public DelegateCommand CountryChangeCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }
        public DelegateCommand CloseCommand { get; private set; }
        public DelegateCommand InsertCommand { get; private set; }
        public DelegateCommand DeleteCommand { get; private set; }
        public DelegateCommand AddEmailCommand { get; private set; }
        public DelegateCommand AddGetReadyCommand { get; private set; }
        public DelegateCommand UpdateGetReadyCommand { get; private set; }
        public DelegateCommand DeleteGetReadyCommand { get; private set; }
        public DelegateCommand ShowGetReadyDetailsCommand { get; private set; }
        public DelegateCommand<GetReadyModel> RowSingleClickCommand { get; private set; }
        public DelegateCommand UpdateCommand { get; private set; }
        public DelegateCommand GetCityCommand { get; private set; }
        public DelegateCommand<string> EffectiveDateLostFocusCommand { get; private set; }
        public DealersNetworkViewModel(IDealerNetwork dealerNetwork, IMasterData masterData, ILogger<DealersNetworkViewModel> logger,
                                      IEventAggregator eventAggregator, IRegionManager regionManager,
                                      IContainerProvider containerProvider) : base(eventAggregator, logger)
        {
            _dealerNetwork = dealerNetwork;
            _masterData = masterData;
            _regionManager = regionManager;
            _containerProvider = containerProvider;
            _printCourtesies = PrintCourtesyList();
            _termsOverrides = TermOverrideList();
            DealerOnLoad();
        }
        
        /// <summary>
        /// To load basic requirements
        /// </summary>
        private void DealerOnLoad()
        {
            IsEnableUpdateDealerButton = false;
            IsEnableDeleteButton = false;
            IsEnableGetReady = false;
            GetReadyModel = new GetReadyModel();
            DealerNetwork = new DealerNetworkModel();
            ContactEmail = new ContactEmailModel();
            CountryChangeCommand = new DelegateCommand(CountryChanged);
            InsertCommand = new DelegateCommand(InsertDealer);
            DeleteCommand = new DelegateCommand(DeleteDealer);
            CancelCommand = new DelegateCommand(CancelDealer);
            CloseCommand = new DelegateCommand(CloseDealer);
            AddEmailCommand = new DelegateCommand(AddEmail);
            UpdateCommand = new DelegateCommand(UpdateDealer);
            GetCityCommand = new DelegateCommand(GetCityFromZip);
            AddGetReadyCommand = new DelegateCommand(InsertGetReady);
            UpdateGetReadyCommand = new DelegateCommand(UpdateGetReady);
            DeleteGetReadyCommand = new DelegateCommand(DeleteGetReady);
            ShowGetReadyDetailsCommand = new DelegateCommand(ViewGetReadyDetails);
            RowSingleClickCommand = new DelegateCommand<GetReadyModel>(EditSelectedGetReady);
            EffectiveDateLostFocusCommand = new DelegateCommand<string>(ValidEffectiveDate);
            PrintCourtesies = new ObservableCollection<PrintCourtesyModel>(_printCourtesies);
            TermsOverrides = new ObservableCollection<TermsOverrideModel>(_termsOverrides);
            GetMakeDetail();
            GetCountryDetail();
            ShowStatusBarMessage();
            SetWindowTitle(DealerConstant.DealerNetworkViewTitle);
        }

        /// <summary>
        /// Add dealer
        /// </summary>
        private async void InsertDealer()
        {
            if (!IsValidate()) return;
            ShowStatusBarMessage(DealerConstant.InsertingRecord);
            LogInformation(DealerConstant.ExecutingInsert);

            if (PrintCourtesy == null || (IsActiveTermsOverride && TermsOverride == null))
            {
                ShowValidationMessage(DealerConstant.NotValidEntry);
            }
            else
            {
                var insertData = await _dealerNetwork.InsertDealerNetwork(DealerNetwork);
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
        private async void UpdateDealer()
        {
            if (!IsValidate()) return;
            ShowStatusBarMessage(DealerConstant.UpdatingRecord);
            LogInformation(DealerConstant.ExecutingUpdate);

            if (PrintCourtesy == null || (IsActiveTermsOverride && TermsOverride == null))
            {
                ShowValidationMessage(DealerConstant.NotValidEntry);
            }
            else
            {
                var updateData = await _dealerNetwork.UpdateDealerNetwork(DealerNetwork);
                if (updateData.IsSuccess)
                {
                    ResetDealer();
                    ShowStatusBarMessage(DealerConstant.UpdatedRecord);
                    LogInformation(DealerConstant.ExecutedUpdate);
                }
                else
                {
                    ShowValidationMessage(DealerConstant.UpdatedDealerRecordFailed);
                    LogWarning(DealerConstant.FailedUpdate);
                }
            }
        }

        /// <summary>
        /// This method is used to delete dealer, passing through the dealer command
        /// </summary>
        private async void DeleteDealer()
        {
            //Empty status bar
            ShowStatusBarMessage();
            if (!IsValidate()) return;
            var messageBoxResult = MessageBox.Show(BaseConstant.DeleteConfirmation,
                BaseConstant.DeleteConfirmationTitle, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                ShowStatusBarMessage(DealerConstant.DeletingDealer);
                LogInformation(DealerConstant.DeletingDealer);
                var deleteData = await _dealerNetwork.DeleteDealerNetwork(DealerNetwork);

                if (deleteData.IsSuccess)
                {
                    ResetDealer();
                    ShowStatusBarMessage(DealerConstant.DeletedDealer);
                    LogInformation(DealerConstant.DeletedDealer);
                }
                else
                {
                    ShowStatusBarMessage();
                    MessageBox.Show(DealerConstant.DeleteDealerFail, BaseConstant.ErrorMessageBoxTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                    LogWarning(DealerConstant.DeleteDealerFail);
                }
            }
        }

        /// <summary>
        /// To check UI validation 
        /// </summary> 
        private bool IsValidate()
        {
            if (!DealerNetwork.HasErrors) return true;
            var errorMessage = string.Join("\n", DealerNetwork?.Errors.Errors.Select(x => string.Join("\n", x.Value)));
            ShowValidationMessage(errorMessage);
            return false;
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
        /// Get country detail from data source
        /// </summary>
        private async void GetCountryDetail()
        {
            var countryData = await _masterData.GetCountryDetail();
            Countries = countryData.Value;
        }

        /// <summary>
        /// Get make detail from data source
        /// </summary>
        private async void GetMakeDetail()
        {
            var makeData = await _masterData.GetMakeDetail();
            _makes.AddRange(makeData.Value);
            Makes = new ObservableCollection<MakeModel>(_makes);
        }

        /// <summary>
        /// Country selection change to fill make code
        /// </summary>
        private void CountryChanged()
        {
            CountryCodeBaseControlEnabled();

            Makes?.Clear();
            _ = !string.IsNullOrEmpty(DealerNetwork.CountryCode) ? Makes?.AddRange(_makes.Where(x => x.CountryCode == DealerNetwork.CountryCode).ToList()) : Makes?.AddRange(_makes);
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

            IsActiveFactry = false;
            IsActiveKd = false;
            IsActiveVolume = false;
            IsActiveBank = false;
            IsActiveTermsOverride = false;

            if (!string.IsNullOrEmpty(DealerNetwork.CountryCode) && (DealerNetwork.CountryCode == "UK" || DealerNetwork.CountryCode == "US" ||
                _userIdList.Contains(_userId.ToUpper().Trim()) || _userDept.ToUpper().Trim() == "MIS"))
            {
                IsActiveTermsOverride = true;
                IsActiveBank = true;

                if (DealerNetwork.CountryCode == "US")
                {
                    var userSecurityId = GetUserSecurityCode(_userId.ToUpper().Trim());
                    if (!_userSecurityList.Contains(userSecurityId) && _userIdSecurityList.Contains(_userId.ToUpper().Trim()))
                    {
                        IsActiveBank = false;                       
                    }

                    if (_userIdUS.Contains(_userId.ToUpper().Trim()) || _userDept.ToUpper().Trim() == "MIS")
                    {
                        IsActiveKd = true;
                    }
                }
                else
                {
                    IsActiveVolume = true;
                }
            }
            if (DealerNetwork.CountryCode == "CA")
            {
                IsActiveFactry = true;
            }
            ResetDisabledProperties();           
        }

        /// <summary>
        ///  Reset Value of disabled dealer's properties
        /// </summary>
        private void ResetDisabledProperties()
        {
            if (IsActiveVolume == false)
            {
                DealerNetwork.CanVolumeStock = null;
                DealerNetwork.CanVolumeFactory = null;
            }
            if (IsActiveKd == false)
            {
                DealerNetwork.KdClient = string.Empty;
                DealerNetwork.KdDiv = string.Empty;
            }
            if (IsActiveTermsOverride == false)
            {
                DealerNetwork.TermsOverride = null;
                SelectedTermOverride = string.Empty;
            }
            if(IsActiveBank==false)
            {
                DealerNetwork.BankAccount = null;
                DealerNetwork.BankCity = null;
                DealerNetwork.BankName = null;
                DealerNetwork.BankNumber = null;
            }
            if (IsActiveFactry == false)
            {
                IsCheckedFactry = false;
            }

            RaisePropertyChanged("DealerNetwork");
            RaisePropertyChanged("GetReadyModel");
        }

        /// <summary>
        /// Close dealer but now now we have redirect it to search dealer page
        /// </summary>
        private void CloseDealer()
        {
            ShowStatusBarMessage();
            ResetDealer();
            _regionManager.RequestNavigate(RegionNames.ContentRegion, ViewNames.DealersSearchView);
            CloseChildWindow();
        }

        /// <summary>
        /// Cancel dealer
        /// </summary>
        private void CancelDealer()
        {
            ShowStatusBarMessage();
            ResetDealer();
            ResetDealerButton();
        }

        /// <summary>
        /// Reset dealer
        /// </summary>
        private void ResetDealer()
        {
            Country = null;
            Make = null;
            TermsOverride = null;
            PrintCourtesy = null;
            SelectedPrintCourtesy = null;
            SelectedTermOverride = null;
            GetReadies = new ObservableCollection<GetReadyModel>();
            GetReadyModel = new GetReadyModel();
            DealerNetwork = new DealerNetworkModel();
            IsEnableGetReady = false;
            VisiblityHeader = "None";
            EffectiveDate = string.Empty;
            CountryChanged();
            DisableGetVehicles();
            ResetDealerButton();
        }

        /// <summary>
        /// Reset Dealer's Buttons
        /// </summary>
        private void ResetDealerButton()
        {
            IsEnableCopyDealerButton = true;
            IsEnableUpdateDealerButton = false;
            IsEnableDeleteButton = false;
            IsEnableCancelButton = true;
        }

        /// <summary>
        /// Called when the implementer has been navigated to.
        /// </summary>
        /// <param name="navigationContext">NavigationContext</param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            ShowStatusBarMessage();
            SetWindowTitle(DealerConstant.DealerNetworkViewTitle);
            IsEnableGetReady = false;
            if (navigationContext.Parameters["dealersData"] is DealerSearchModel dealersData)
            {
                SelectedDealer = dealersData;
                NavigateWithData(dealersData);
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// This method is used to navigate to dealer network window with data 
        /// </summary>
        /// <param name="dealersData"></param>
        private void NavigateWithData(DealerSearchModel dealersData)
        {
            DealerNetwork = new DealerNetworkModel()
            {
                CountryCode = dealersData.CountryCode,
                MakeCode = dealersData.MakeCode,
                DealerId = dealersData.DealerId,
            };
            GetDealerNetwork(DealerNetwork);
        }

        /// <summary>
        /// Get dealer detail for edit
        /// </summary>
        /// <param name="networkModel">DealersNetworkModel</param>
        private async void GetDealerNetwork(DealerNetworkModel networkModel)
        {
            var dealerNetworkData = await _dealerNetwork.GetDealerNetwork(networkModel);
            DealerNetwork = dealerNetworkData.Value.FirstOrDefault();
            SelectedCountry = DealerNetwork?.CountryCode;
            SelectedMake = DealerNetwork?.MakeCode;
            SelectedPrintCourtesy = DealerNetwork?.CourtesyDelivPrintInd;
            SelectedTermOverride = DealerNetwork?.TermsOverride;
            ResetGetReady();
            GetReadyDetails();
            CountryCodeBaseControlEnabled();
            new Task(() => { IsEnableGetReady = true; }).Start();
            IsEnableCopyDealerButton = false;
            IsEnableUpdateDealerButton = true;
            IsEnableDeleteButton = true;
            IsEnableCancelButton = true;
        }

        /// <summary>
        /// Called to determine if this instance can handle the navigation request.
        /// </summary>
        /// <param name="navigationContext">NavigationContext</param>
        /// <returns>bool</returns>
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters["dealersData"] is DealerSearchModel dealersData)
            {
                return SelectedDealer != null && SelectedDealer == dealersData;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Called when the implementer has been navigated away from.
        /// </summary>
        /// <param name="navigationContext">NavigationContext</param>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            return;
        }

        /// <summary>
        /// This method is used to navigate to additional email window.
        /// </summary>
        private void AddEmail()
        {
            if (IsValidate())
            {
                GetContactEmail();
                var dialog = _containerProvider.Resolve<ContactEmailView>();
                _eventAggregator.GetEvent<DealerAggregator>().Publish(DealerNetwork);
                dialog.ShowInTaskbar = false;
                dialog.Owner = Application.Current.MainWindow;
                dialog.Title = ContactEmailConstant.ContactEmailViewTitle;
                dialog.ShowDialog();
            }
        }

        /// <summary>
        /// Get contact email details
        /// </summary>
        private async void GetContactEmail()
        {
            var contactDetails = await _dealerNetwork.GetContactEmailDetail(DealerNetwork.CountryCode, DealerNetwork.MakeCode, DealerNetwork.DealerId);
            ContactEmail.StockContactName = contactDetails.Value
                .Where(x => x.ContactType == ContactType.Stock.ToString()).Select(x => x.ContactName).FirstOrDefault();
            ContactEmail.StockContactEmail = contactDetails.Value
                .Where(x => x.ContactType == ContactType.Stock.ToString()).Select(x => x.ContactEmail).FirstOrDefault();
            ContactEmail.FinanceContactName = contactDetails.Value
                .Where(x => x.ContactType == ContactType.Finance.ToString()).Select(x => x.ContactName).FirstOrDefault();
            ContactEmail.FinanceContactEmail = contactDetails.Value
                .Where(x => x.ContactType == ContactType.Finance.ToString()).Select(x => x.ContactEmail).FirstOrDefault();
            ContactEmail.LicenseContactName = contactDetails.Value
                .Where(x => x.ContactType == ContactType.Licence.ToString()).Select(x => x.ContactName).FirstOrDefault();
            ContactEmail.LicenseContactEmail = contactDetails.Value
                .Where(x => x.ContactType == ContactType.Licence.ToString()).Select(x => x.ContactEmail).FirstOrDefault();
            DealerNetwork.ContactEmail = ContactEmail;
        }

        /// <summary>
        /// Get City,State from zip code
        /// </summary>
        private async void GetCityFromZip()
        {
            var result = await _dealerNetwork.GetCityFromZip(DealerNetwork.ZipCode);
            if (result.HasValue == false)
            {
                ShowValidationMessage(DealerConstant.ZipCode);
                DealerNetwork.City = string.Empty;
                DealerNetwork.State = string.Empty;
            }
            else
            {
                DealerNetwork.City = result.Value.City;
                DealerNetwork.State = result.Value.State;
            }
            RaisePropertyChanged("DealerNetwork");
        }

        /// <summary>
        ///  Insert Get Ready's Details
        /// </summary>
        private async void InsertGetReady()
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
        /// Set insert get ready process
        /// </summary>
        /// <returns></returns>
        private async Task SetInsertGetReadyProcess()
        {
            try
            {
                var insertData = await _dealerNetwork.InsertGetReady(GetReadyModel);

                if (insertData.IsSuccess)
                {
                    ShowStatusBarMessage(DealerConstant.InsertedGetReadyRecord);
                    LogInformation(DealerConstant.ExecutedGetReadyInsert);
                    GetReadyModel.GetReadyCategories = new List<string>();
                    ResetGetReady();
                    GetReadyDetails();
                }
                else
                {
                    LogWarning(DealerConstant.FailedGetReadyInsert);
                }
            }
            catch
            {
                ResetGetReady();
                GetReadyDetails();
                throw;
            }
        }

        /// <summary>
        ///  Update Get Ready's Details
        /// </summary>
        private async void UpdateGetReady()
        {
            if (IsValidateGetReady())
            {
                _isGREdit = false;
                ShowStatusBarMessage(DealerConstant.UpdatingGetReadyRecord);
                LogInformation(DealerConstant.ExecutingGetReadyUpdate);
                SetGetReadyDetails();
                SetEffectiveDate();
                await SetUpdateGetReady();
            }
        }

        /// <summary>
        /// Set update get ready process
        /// </summary>
        /// <returns></returns>
        private async Task SetUpdateGetReady()
        {
            try
            {
                var updateData = await _dealerNetwork.UpdateGetReady(GetReadyModel);
                if (updateData.IsSuccess)
                {
                    ShowStatusBarMessage(DealerConstant.UpdatedGetReadyRecord);
                    LogInformation(DealerConstant.ExecutedGetReadyUpdate);
                    GetReadyModel.GetReadyCategories = new List<string>();
                    ResetGetReady();
                    GetReadyDetails();
                }
                else
                {
                    LogWarning(DealerConstant.FailedGetReadyUpdate);
                }
            }
            catch
            {
                ResetGetReady();
                GetReadyDetails();
                _isGREdit = true;
                throw;
            }
        }

        /// <summary>
        ///  Delete Get Ready's Details
        /// </summary>
        private async void DeleteGetReady()
        {
            if (IsValidateGetReady())
            {
                SetGetReadyDetails();

                if (GetReadyModel != null && string.IsNullOrEmpty(GetReadyModel.GetReadyCategory))
                {
                    ShowValidationMessage(DealerConstant.DeletedGetReadyRecordFailed);
                }
                else
                {
                    var messageBoxResult = MessageBox.Show(BaseConstant.DeleteConfirmation, BaseConstant.DeleteConfirmationTitle, MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        _isGREdit = false;
                        ShowStatusBarMessage(DealerConstant.DeletingGetReadyRecord);
                        LogInformation(DealerConstant.ExecutingGetReadyDelete);
                        await SetDeleteGetReadyProcess();
                    }
                }
            }
        }

        /// <summary>
        /// Set delete get ready process
        /// </summary>
        /// <returns></returns>
        private async Task SetDeleteGetReadyProcess()
        {
            try
            {
                var deleteData = await _dealerNetwork.DeleteGetReady(GetReadyModel);
                if (deleteData.IsSuccess)
                {
                    ShowStatusBarMessage(DealerConstant.DeletedGetReadyRecord);
                    LogInformation(DealerConstant.ExecutedGetReadyDelete);
                    ResetGetReady();
                    GetReadyDetails();
                }
                else
                {
                    LogWarning(DealerConstant.FailedGetReadyDelete);
                }
            }
            catch
            {
                ResetGetReady();
                GetReadyDetails();
                _isGREdit = true;
                throw;
            }
        }

        /// <summary>
        /// Set Effective date when found blank
        /// </summary>
        private void SetEffectiveDate()
        {
            if (GetReadyModel.GetReadyEffectiveDate == null)
            {
                EffectiveDate = DateTime.Now.Date.ToString("MM'/'dd'/'yyyy");
                GetReadyModel.GetReadyEffectiveDate = DateTime.ParseExact(EffectiveDate, "MM'/'dd'/'yyyy", CultureInfo.InvariantCulture);
            }
        }
        /// <summary>
        /// To check ui validation 
        /// </summary> 
        private bool IsValidateGetReady()
        {
            if (IsValidate() == false)
            {
                return false;
            }

            if (GetReadyModel.HasErrors)
            {
                string errorMessage = string.Join("\n", GetReadyModel?.Errors.Errors.Select(x => string.Join("\n", x.Value)));
                ShowValidationMessage(errorMessage);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Show Get Rady Details on UI
        /// </summary>
        private void ViewGetReadyDetails()
        {
            if (_isGREdit)
            {
                GetReadyDetails();
                _isGREdit = false;
            }
        }

        /// <summary>
        ///  Get/Set selected row from Get Ready Details
        /// </summary>
        /// <param name="getReadyListModel"></param>
        private void EditSelectedGetReady(GetReadyModel getReadyListModel)
        {
            BindGetReadyDetail(getReadyListModel);
        }

        /// <summary>
        ///  Bind selected row details of Get Ready 
        /// </summary>
        /// <param name="getReadyListModel"></param>
        private void BindGetReadyDetail(GetReadyModel getReadyListModel)
        {
            DisableGetVehicles();
            if (getReadyListModel.GetReadyEffectiveDate != null)
            {
                string effectiveDate = DateTime.Parse(getReadyListModel.GetReadyEffectiveDate.ToString()).ToString("MM'/'dd'/'yyyy");

                GetReadyModel.GetReadyEffectiveDate = DateTime.ParseExact(effectiveDate, "MM'/'dd'/'yyyy", CultureInfo.InvariantCulture);
                EffectiveDate = effectiveDate;
            }
            else
            {
                GetReadyModel.GetReadyEffectiveDate = null;
                EffectiveDate = string.Empty;
            }
            GetReadyModel.ClientId = getReadyListModel.ClientId;
            GetReadyModel.GetReadyAmount = getReadyListModel.GetReadyAmount;
            CheckGetReadyCategory(getReadyListModel);
            RaisePropertyChanged("GetReadyModel");
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
                    IsCheckedCars = true;
                    break;
                case "Trucks":
                    IsCheckedTrucks = true;
                    break;
                case "Local":
                    IsCheckedLocal = true;
                    break;
                case "Luxury":
                    IsCheckedLuxury = true;
                    break;
                case "Factry":
                    IsCheckedFactry = true;
                    break;
                case "Stocks":
                    IsCheckedStocks = true;
                    break;
            }
        }

        /// <summary>
        /// Set other details of Get Ready
        /// </summary>
        private void SetGetReadyDetails()
        {
            List<string> vehicles = new List<string>();
            if (IsCheckedCars)
            {
                vehicles.Add("Cars");
                GetReadyModel.GetReadyCategory = "Cars";
            }
            if (IsCheckedTrucks)
            {
                vehicles.Add("Trucks");
                GetReadyModel.GetReadyCategory = "Trucks";
            }
            if (IsCheckedLocal)
            {
                vehicles.Add("Local");
                GetReadyModel.GetReadyCategory = "Local";
            }
            if (IsCheckedLuxury)
            {
                vehicles.Add("Luxury");
                GetReadyModel.GetReadyCategory = "Luxury";
            }
            if (IsCheckedFactry)
            {
                vehicles.Add("Factry");
                GetReadyModel.GetReadyCategory = "Factry";
            }
            if (IsCheckedStocks)
            {
                vehicles.Add("Stocks");
                GetReadyModel.GetReadyCategory = "Stocks";
            }
            if (vehicles.Count > 0)
            {
                GetReadyModel.GetReadyCategories = vehicles;
            }
            if (string.IsNullOrEmpty(GetReadyModel.ClientId) || GetReadyModel.ClientId == " ")
            {
                GetReadyModel.ClientId = "*";
            }
            if (GetReadyModel.GetReadyAmount == null)
            {
                GetReadyModel.GetReadyAmount = 0;
            }
        }

        /// <summary>
        /// Get Getready Details
        /// </summary>
        private async void GetReadyDetails()
        {
            SetCompositeKey();
            var getReadyList = await _dealerNetwork.GetReadyDetails(GetReadyModel);
            GetReadies = getReadyList.Value;
            IsEnableGetReadyList = true;
            VisiblityHeader = "All";       
            if (GetReadies.Count == 0)
            {
                IsEnableGetReadyList = false;
                VisiblityHeader = "None";

            }
            RaisePropertyChanged("GetReadyList");
            RaisePropertyChanged("GetReadyModel");
        }

        /// <summary>
        ///  Reset Get Ready contains
        /// </summary>
        private void ResetGetReady()
        {
            IsEnableGetReadyList = false;
            DisableGetVehicles();
            GetReadyModel.ClientId = string.Empty;
            GetReadyModel.GetReadyEffectiveDate = null;
            GetReadyModel.GetReadyAmount = null;
            GetReadyModel.GetReadyCategories = null;
            GetReadyModel.GetReadyCategory = string.Empty;
            GetReadyModel.GetReadyEffectiveDate = null;
            EffectiveDate = string.Empty;
        }

        /// <summary>
        ///  Disable Checkbox for Gr Vehicles
        /// </summary>
        private void DisableGetVehicles()
        {
            IsCheckedCars = false;
            IsCheckedTrucks = false;
            IsCheckedLocal = false;
            IsCheckedFactry = false;
            IsCheckedStocks = false;
            IsCheckedLuxury = false;
        }
        
        /// <summary>
        ///  Set composite key for get ready
        /// </summary>
        private void SetCompositeKey()
        {
            GetReadyModel.CountryCode = DealerNetwork.CountryCode;
            GetReadyModel.MakeCode = DealerNetwork.MakeCode;
            GetReadyModel.DealerId = DealerNetwork.DealerId;
        }

        /// <summary>
        ///  Check Valid Effective Date
        /// </summary>
        /// <param name="effectiveDate"></param>
        private void ValidEffectiveDate(string effectiveDate)
        {
            if (!string.IsNullOrEmpty(effectiveDate))
            {
                var _date = new DateTime();
                var testResult = DateTime.TryParseExact(effectiveDate, "MM'/'dd'/'yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _date);
                if (testResult == false)
                {
                    ShowValidationMessage(BaseConstant.EffectDateValidateErrorMessage);
                    EffectiveDate = string.Empty;
                }
                else
                {
                    GetReadyModel.GetReadyEffectiveDate = DateTime.ParseExact(effectiveDate, "MM'/'dd'/'yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
                }
            }
            else
            {
                GetReadyModel.GetReadyEffectiveDate = null;               
            }
        }
        /// <summary>
        /// Set Print Courtesy List
        /// </summary>
        /// <returns></returns>
        private List<PrintCourtesyModel> PrintCourtesyList()
        {
            return new List<PrintCourtesyModel> {
                new PrintCourtesyModel { PrintCourtesy = "Fax", Description = "Fax" },
                new PrintCourtesyModel { PrintCourtesy = "Email", Description = "Email" }
            };
        }

        /// <summary>
        /// Set Term Override List
        /// </summary>
        /// <returns></returns>
        private List<TermsOverrideModel> TermOverrideList()
        {
            return new List<TermsOverrideModel> {
                new TermsOverrideModel { TermsOverride = "Due01", Description = "Due01" },
                new TermsOverrideModel { TermsOverride = "Due07", Description = "Due07" },
                new TermsOverrideModel { TermsOverride = "Due10", Description = "Due10" },
                new TermsOverrideModel { TermsOverride = "Due15", Description = "Due15" },
                new TermsOverrideModel { TermsOverride = "Due20", Description = "Due20" },
                new TermsOverrideModel { TermsOverride = "Due25", Description = "Due25" },
                new TermsOverrideModel { TermsOverride = "Due30", Description = "Due30" },
                new TermsOverrideModel { TermsOverride = "Rcv25", Description = "Rcv25" },
                new TermsOverrideModel { TermsOverride = "Rec45", Description = "Rec45" },
                new TermsOverrideModel { TermsOverride = "10N1P", Description = "10N1P" }
            };
        }

        /// <summary>
        /// Close Child window open
        /// </summary>
        private void CloseChildWindow()
        {
            if (Application.Current?.MainWindow != null)
            {
                foreach (var window in from Window window in Application.Current.Windows
                                       where window is ContactEmailView
                                       select window)
                {
                    window.Close();
                }
            }
        }
    }
}

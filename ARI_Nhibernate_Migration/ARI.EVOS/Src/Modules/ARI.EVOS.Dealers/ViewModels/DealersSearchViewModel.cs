using ARI.EVOS.Common.Models;
using ARI.EVOS.Dealer.AppServices.Interface;
using ARI.EVOS.Dealers.Models;
using ARI.EVOS.Infra;
using ARI.EVOS.Infra.ViewModel;
using Microsoft.Extensions.Logging;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using ARI.EVOS.Dealers.Constant;

namespace ARI.EVOS.Dealers.ViewModels
{
    /// <summary>
    /// This class is used to interact with dealer search view
    /// </summary>
    public class DealersSearchViewModel : BaseViewModel, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly IDealerNetwork _dealerNetwork;
        private readonly IMasterData _masterData;

        private bool _isActiveDataGrid = true;
        private DealerSearchModel _dealerSearchModel;
        private CountryModel _countryModel;
        private MakeModel _makeModel;
        private ObservableCollection<DealerSearchModel> _dealers = new ObservableCollection<DealerSearchModel>();
        private readonly List<MakeModel> _makes = new List<MakeModel>();

        public ObservableCollection<DealerSearchModel> Dealers
        {
            get => _dealers;
            set
            {
                try { SetProperty(ref _dealers, value); }
                catch { }
            }
        }
        public DealerSearchModel DealersSearch
        {
            get => _dealerSearchModel;
            set => SetProperty(ref _dealerSearchModel, value);
        }
        public CountryModel Country
        {
            get => _countryModel;
            set
            {
                SetProperty(ref _countryModel, value);
                DealersSearch.CountryCode = _countryModel?.CountryCode;
            }
        }
        public MakeModel Make
        {
            get => _makeModel;
            set
            {
                SetProperty(ref _makeModel, value);
                DealersSearch.MakeCode = _makeModel?.MakeCode;
            }
        }


        public bool IsActiveDataGrid
        {
            get => _isActiveDataGrid;
            set
            {
                _isActiveDataGrid = value;
                RaisePropertyChanged("IsActiveDataGrid");
            }
        }
        public ObservableCollection<CountryModel> Countries { get; set; }
        public ObservableCollection<MakeModel> Makes { get; set; }
        public DelegateCommand SearchCommand { get; private set; }
        public DelegateCommand AddDealerCommand { get; private set; }
        public DelegateCommand ClearSelectionCommand { get; private set; }
        public DelegateCommand ExitCommand { get; private set; }
        public DelegateCommand<DealerSearchModel> SelectDealerCommand { get; private set; }
        public DelegateCommand CountryChangeCommand { get; private set; }

        public DealersSearchViewModel(IDealerNetwork dealerNetwork, IMasterData masterData, IRegionManager regionManager,
                                     IEventAggregator eventAggregator, ILogger<DealersSearchViewModel> logger) : base(eventAggregator, logger)
        {
            _regionManager = regionManager;
            _dealerNetwork = dealerNetwork;
            _masterData = masterData;
            DealerOnLoad();
        }

        /// <summary>
        /// To load basic requirements
        /// </summary>
        private void DealerOnLoad()
        {
            DealersSearch = new DealerSearchModel();
            SearchCommand = new DelegateCommand(ViewDealers);
            AddDealerCommand = new DelegateCommand(NavigateToDealerNetWork);
            ClearSelectionCommand = new DelegateCommand(ClearSelection);
            ExitCommand = new DelegateCommand(Exit);
            SelectDealerCommand = new DelegateCommand<DealerSearchModel>(EditSelectedDealer);
            CountryChangeCommand = new DelegateCommand(CountryChanged);
            GetCountryDetail();
            GetMakeDetail();
            ViewDealersList();
            ShowStatusBarMessage(DealerConstant.SearchDealer);
            SetWindowTitle(DealerConstant.DealerSearchViewTitle);
        }

        /// <summary>
        /// This method is used to view dealers list
        /// </summary>
        private async void ViewDealersList()
        {
            var dealers = await _dealerNetwork.GetDealersList();            
            Dealers = dealers.Value;
        }

        /// <summary>
        /// This method is used to view dealers list by search criteria
        /// </summary>
        private async void ViewDealers()
        {
            var dealers = await _dealerNetwork.SearchDealers(DealersSearch);
            if (dealers.HasValue)
            {
                IsActiveDataGrid = true;
                if (Dealers != null) { Dealers.Clear(); }
                if (dealers.Value.Count > 0) { Dealers = dealers.Value; }
                else
                {
                    DisableDataGrid();
                    ShowValidationMessage(DealerConstant.RecordsNotFound);
                }
            }
        }

        /// <summary>
        /// This method is used to disable datagrid
        /// </summary>
        private void DisableDataGrid()
        {
            IsActiveDataGrid = false;
        }

        /// <summary>
        /// This method is used to navigate to add dealer screen
        /// </summary>
        private void NavigateToDealerNetWork()
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, ViewNames.DealersNetworkView);
            ShowStatusBarMessage(DealerConstant.AddingDealer);
        }

        /// <summary>
        /// This method is used to clear selection
        /// </summary>
        private void ClearSelection()
        {
            IsActiveDataGrid = true;
            DealersSearch = new DealerSearchModel();
            Country = null;
            Make = null;
            ViewDealersList();
        }

        /// <summary>
        /// This method is used to exit from window
        /// </summary>
        private void Exit()
        {
            if (Application.Current?.MainWindow != null) Application.Current.MainWindow.Close();
        }

        /// <summary>
        /// This method is used to edit dealer details
        /// </summary>
        private void EditSelectedDealer(DealerSearchModel dealersSearchModel)
        {
            var parameters = new NavigationParameters { { "dealersData", dealersSearchModel } };
            if (dealersSearchModel != null)
                _regionManager.RequestNavigate(RegionNames.ContentRegion, ViewNames.DealersNetworkView, parameters);
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
            Makes?.Clear();
            _ = !string.IsNullOrEmpty(DealersSearch.CountryCode)
                ? Makes?.AddRange(_makes.Where(x => x.CountryCode == DealersSearch.CountryCode).ToList())
                : Makes?.AddRange(_makes);
        }

        /// <summary>
        /// Called when the implementer has been navigated to.
        /// </summary>
        /// <param name="navigationContext">NavigationContext</param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            ClearSelection();
            ShowStatusBarMessage(DealerConstant.SearchDealer);
            SetWindowTitle(DealerConstant.DealerSearchViewTitle);
        }

        /// <summary>
        /// Called to determine if this instance can handle the navigation request.
        /// </summary>
        /// <param name="navigationContext">NavigationContext</param>
        /// <returns></returns>
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        /// <summary>
        /// Called when the implementer has been navigated away from.
        /// </summary>
        /// <param name="navigationContext">NavigationContext</param>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            return;
        }
    }
}

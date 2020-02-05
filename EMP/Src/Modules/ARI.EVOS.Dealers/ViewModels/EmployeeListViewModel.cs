using ARI.EVOS.Common.Models;
using ARI.EVOS.Infra;
using ARI.EVOS.Infra.ViewModel;
using EMP.Management.AppServices.Interface;
using EMP.Management.Constant;
using EMP.Management.Models;
using Microsoft.Extensions.Logging;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Windows;

namespace EMP.Management.ViewModels
{
    public class EmployeeListViewModel : BaseViewModel, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly IEmployee _employee;
        private readonly IMasterData _masterData;

        private EmployeeModel _employeeModel;
        private CountryModel _countryModel;
        private ObservableCollection<EmployeeModel> _employees = new ObservableCollection<EmployeeModel>();
        private bool _isActiveDataGrid = true;

        public ObservableCollection<EmployeeModel> Employees
        {
            get => _employees;
            set
            {
                try { SetProperty(ref _employees, value); }
                catch { }
            }
        }
        public EmployeeModel EmployeeSearch
        {
            get => _employeeModel;
            set => SetProperty(ref _employeeModel, value);
        }
        public CountryModel Country
        {
            get => _countryModel;
            set
            {
                SetProperty(ref _countryModel, value);
                EmployeeSearch.CountryCode = _countryModel?.CountryCode;
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
        public DelegateCommand SearchCommand { get; private set; }
        public DelegateCommand ClearSelectionCommand { get; private set; }
        public DelegateCommand AddEmployeeCommand { get; private set; }
        public DelegateCommand ExitCommand { get; private set; }
        public DelegateCommand<EmployeeModel> SelectEmployeeCommand { get; private set; }
        public EmployeeListViewModel(IEmployee employee, IMasterData masterData, IRegionManager regionManager,
                                     IEventAggregator eventAggregator, ILogger<EmployeeListViewModel> logger) : base(eventAggregator, logger)
        {
            _regionManager = regionManager;
            _employee = employee;
            _masterData = masterData;
            EmployeeOnLoad();
        }

        private void EmployeeOnLoad()
        {
            EmployeeSearch = new EmployeeModel();
            SearchCommand = new DelegateCommand(ViewEmployees);
            ClearSelectionCommand = new DelegateCommand(ClearSelection);
            AddEmployeeCommand = new DelegateCommand(NavigateToEmployeeForm);
            ExitCommand = new DelegateCommand(Exit);
            SelectEmployeeCommand = new DelegateCommand<EmployeeModel>(EditSelectedEmployee);
            GetCountryDetail();
            ViewEmployeesList();
            ShowStatusBarMessage(EmployeeConstant.SearchEmployee);
            SetWindowTitle(EmployeeConstant.EmployeeListViewTitle);
        }
        private async void ViewEmployeesList()
        {
            var employees = await _employee.GetEmployeeList();
            Employees = employees.Value;
        }
        private async void ViewEmployees()
        {
            var employees = await _employee.SearchEmployee(EmployeeSearch);
            if (employees.HasValue)
            {
                IsActiveDataGrid = true;
                if (Employees != null) { Employees.Clear(); }
                if (employees.Value.Count > 0) { Employees = employees.Value; }
                else
                {
                    DisableDataGrid();
                    ShowValidationMessage(EmployeeConstant.RecordsNotFound);
                }
            }
        }
        private void DisableDataGrid()
        {
            IsActiveDataGrid = false;
        }
        private void NavigateToEmployeeForm()
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, ViewNames.EmployeeFormView);
            ShowStatusBarMessage(EmployeeConstant.AddingEmployee);
        }
        private void ClearSelection()
        {
            IsActiveDataGrid = true;
            EmployeeSearch = new EmployeeModel();
            Country = null;
            ViewEmployeesList();
        }
        private void Exit()
        {
            if (Application.Current?.MainWindow != null) Application.Current.MainWindow.Close();
        }
        private void EditSelectedEmployee(EmployeeModel employeeModel)
        {
            var parameters = new NavigationParameters { { "employeeData", employeeModel } };
            if (employeeModel != null)
                _regionManager.RequestNavigate(RegionNames.ContentRegion, ViewNames.EmployeeFormView, parameters);
        }
        private async void GetCountryDetail()
        {
            var countryData = await _masterData.GetCountryDetail();
            Countries = countryData.Value;
        }
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            return;
        }
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            ClearSelection();
            ShowStatusBarMessage(EmployeeConstant.SearchEmployee);
            SetWindowTitle(EmployeeConstant.EmployeeListViewTitle);
        }
    }
}

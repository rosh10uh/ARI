using ARI.EVOS.Common.Models;
using ARI.EVOS.Dealers.Views;
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
using System.Linq;
using System.Windows;

namespace ARI.EVOS.Dealers.ViewModels
{
    public class EmployeeFormViewModel : BaseViewModel, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly IEmployee _employee;
        private readonly IMasterData _masterData;

        private CountryModel _countryModel;
        private EmployeeModel _employeeModel;
        private EmployeeModel _selectedEmployee;
        private ObservableCollection<EmployeeModel> _employees = new ObservableCollection<EmployeeModel>();
        private string _selectedCountry = string.Empty;
        private string _visiblityHeader = "None";
        private bool _isActiveDataGrid = true;

        public ObservableCollection<EmployeeModel> Employee
        {
            get => _employees;
            set
            {
                try { SetProperty(ref _employees, value); }
                catch { }
            }
        }
        public EmployeeModel EmployeeModel
        {
            get => _employeeModel;
            set
            {
                try { SetProperty(ref _employeeModel, value); }
                catch { }
            }
        }
        public EmployeeModel SelectedEmployee
        {
            get => _selectedEmployee;
            set => SetProperty(ref _selectedEmployee, value);
        }
        public CountryModel Country
        {
            get => _countryModel;
            set
            {
                SetProperty(ref _countryModel, value);
                EmployeeModel.CountryCode = _countryModel?.CountryCode;
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
        public bool IsActiveDataGrid
        {
            get => _isActiveDataGrid;
            set
            {
                _isActiveDataGrid = value;
                RaisePropertyChanged("IsActiveDataGrid");
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

        public ObservableCollection<CountryModel> Countries { get; set; }
        public DelegateCommand CountryChangeCommand { get; private set; }
        public DelegateCommand InsertCommand { get; private set; }
        public DelegateCommand DeleteCommand { get; private set; }
        public DelegateCommand UpdateCommand { get; private set; }
        public DelegateCommand CloseCommand { get; private set; }
        public DelegateCommand ClearCommand { get; private set; }

        public EmployeeFormViewModel(IEmployee employee, ILogger<EmployeeModel> logger, IRegionManager regionManager, IMasterData masterData,
                                     IEventAggregator eventAggregator) : base(eventAggregator, logger)
        {
            _employee = employee;
            _regionManager = regionManager;
            _masterData = masterData;
            EmployeeOnLoad();
        }

        private void EmployeeOnLoad()
        {
            EmployeeModel = new EmployeeModel();
            InsertCommand = new DelegateCommand(InsertEmployee);
            UpdateCommand = new DelegateCommand(UpdateEmployee);
            DeleteCommand = new DelegateCommand(DeleteEmployee);
            CloseCommand = new DelegateCommand(CloseEmployee);
            ClearCommand = new DelegateCommand(ClearEmployee);
            GetCountryDetail();
        }
        private void ClearEmployee()
        {
            ShowStatusBarMessage();
            ResetEmployee();
        }
        private void ResetEmployee()
        {
            Country = null;
            EmployeeModel = new EmployeeModel();
            VisiblityHeader = "None";
        }
        private void CloseEmployee()
        {
            ShowStatusBarMessage();
            ResetEmployee();
            _regionManager.RequestNavigate(RegionNames.ContentRegion, ViewNames.EmployeeListView);
            CloseChildWindow();
        }
        private async void UpdateEmployee()
        {
            if (!IsValidate()) return;
            ShowStatusBarMessage(EmployeeConstant.UpdatingRecord);
            LogInformation(EmployeeConstant.ExecutingUpdate);

            var updateData = await _employee.UpdateEmployeeDetail(EmployeeModel);
            if (updateData.IsSuccess)
            {

                ShowStatusBarMessage(EmployeeConstant.UpdatedRecord);
                LogInformation(EmployeeConstant.ExecutedUpdate);
            }
            else
            {
                ShowValidationMessage(EmployeeConstant.UpdatedEmployeeRecordFailed);
                LogWarning(EmployeeConstant.FailedUpdate);
            }
        }
        private async void GetCountryDetail()
        {
            var countryData = await _masterData.GetCountryDetail();
            Countries = countryData.Value;
        }
        private async void DeleteEmployee()
        {
            //Empty status bar
            ShowStatusBarMessage();
            if (!IsValidate()) return;
            var messageBoxResult = MessageBox.Show(BaseConstant.DeleteConfirmation,
                BaseConstant.DeleteConfirmationTitle, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                ShowStatusBarMessage(EmployeeConstant.DeletingEmployee);
                LogInformation(EmployeeConstant.DeletingEmployee);
                var deleteData = await _employee.DeleteEmployeeDetail(EmployeeModel);

                if (deleteData.IsSuccess)
                {
                    ResetEmployee();
                    ShowStatusBarMessage(EmployeeConstant.DeletedEmployee);
                    LogInformation(EmployeeConstant.DeletedEmployee);
                }
                else
                {
                    ShowStatusBarMessage();
                    MessageBox.Show(EmployeeConstant.DeleteEmployeeFail, BaseConstant.ErrorMessageBoxTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                    LogWarning(EmployeeConstant.DeleteEmployeeFail);
                }
            }
        }
        private async void InsertEmployee()
        {
            if (!IsValidate()) return;
            ShowStatusBarMessage(EmployeeConstant.InsertingRecord);
            LogInformation(EmployeeConstant.ExecutingInsert);

            var insertData = await _employee.InsertEmployeeDetail(EmployeeModel);

            if (insertData.IsSuccess)
            {
                ShowStatusBarMessage(EmployeeConstant.InsertedRecord);
                LogInformation(EmployeeConstant.ExecutedInsert);
            }
            else
            {
                ShowStatusBarMessage(EmployeeConstant.InsertedFailRecord);
                LogWarning(EmployeeConstant.FailedInsert);
            }
        }

        private bool IsValidate()
        {
            if (!EmployeeModel.HasErrors) return true;
            var errorMessage = string.Join("\n", EmployeeModel?.Errors.Errors.Select(x => string.Join("\n", x.Value)));
            ShowValidationMessage(errorMessage);
            return false;
        }
        private void NavigateWithData(EmployeeModel employeeData)
        {
            EmployeeModel = new EmployeeModel()
            {
                CountryCode = employeeData.CountryCode,
                EmployeeId = employeeData.EmployeeId

            };
            GetEmployee(EmployeeModel);
        }
        private async void GetEmployee(EmployeeModel employeeModel)
        {
            var employeeData = await _employee.SearchEmployee(employeeModel);
            EmployeeModel = employeeData.Value.FirstOrDefault();
            SelectedCountry = EmployeeModel?.CountryCode;
        }
        private void CloseChildWindow()
        {
            if (Application.Current?.MainWindow != null)
            {
                foreach (var window in from Window window in Application.Current.Windows
                                       where window is EmployeeFormView
                                       select window)
                {
                    window.Close();
                }
            }
        }
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters["employeesData"] is EmployeeModel employeeModel)
            {
                return SelectedEmployee != null && SelectedEmployee == employeeModel;
            }
            else
            {
                return true;
            }
        }
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            return;
        }
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            ShowStatusBarMessage();
            SetWindowTitle(EmployeeConstant.EmployeeViewTitle);
            if (navigationContext.Parameters["employeeData"] is EmployeeModel employeeData)
            {
                SelectedEmployee = employeeData;
                NavigateWithData(employeeData);
            }
            else
            {
                return;
            }
        }
    }
}

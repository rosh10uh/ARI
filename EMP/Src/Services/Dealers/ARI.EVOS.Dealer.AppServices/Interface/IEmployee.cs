using CSharpFunctionalExtensions;
using EMP.Management.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace EMP.Management.AppServices.Interface
{
    public interface IEmployee
    {
        Task<Maybe<ObservableCollection<EmployeeModel>>> SearchEmployee(EmployeeModel employeeModel);
        Task<Maybe<ObservableCollection<EmployeeModel>>> GetEmployeeList();
        Task<Result<string>> InsertEmployeeDetail(EmployeeModel  employeeModel);
        Task<Result<string>> DeleteEmployeeDetail(EmployeeModel employeeModel);
        Task<Result<string>> UpdateEmployeeDetail(EmployeeModel employeeModel);
    }
}

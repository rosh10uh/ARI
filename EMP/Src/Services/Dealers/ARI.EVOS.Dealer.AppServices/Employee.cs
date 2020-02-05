using ARI.EVOS.Dealer.AppServices;
using Chassis.Command.Interfaces;
using Chassis.Dapper.Interfaces.Query;
using CSharpFunctionalExtensions;
using EMP.Management.AppServices.Interface;
using EMP.Management.Command.Commands;
using EMP.Management.Models;
using EMP.Management.Query.Queries;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace EMP.Management.AppServices
{
    public class Employee : BaseAppService, IEmployee
    {
        public Employee(ICommandBus commandBus, IQueryDispatcher queryDispatcher) : base(commandBus, queryDispatcher)
        {

        }
        public Task<Result<string>> DeleteEmployeeDetail(EmployeeModel employeeModel)
        {
            return DispatchCommand<DeleteEmployeeCommand, string>(employeeModel);
        }

        public Task<Maybe<ObservableCollection<EmployeeModel>>> SearchEmployee(EmployeeModel employeeModel)
        {
            return DispatchQuery(new GetEmployeeQuery(employeeModel.EmployeeId, employeeModel.CountryCode));
        }

        public Task<Maybe<ObservableCollection<EmployeeModel>>> GetEmployeeList()
        {
            return DispatchQuery(new GetEmployeeListQuery());
        }

        public Task<Result<string>> InsertEmployeeDetail(EmployeeModel employeeModel)
        {
            return DispatchCommand<AddEmployeeCommand, string>(employeeModel);
        }

        public Task<Result<string>> UpdateEmployeeDetail(EmployeeModel employeeModel)
        {
            return DispatchCommand<UpdateEmployeeCommand, string>(employeeModel);
        }
    }
}

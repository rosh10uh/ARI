using ARI.EVOS.Dealer.Domain.Models.Dealer.Entity;
using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using Chassis.Command.Interfaces;
using Chassis.RegisterServices.Attributes;
using CSharpFunctionalExtensions;
using EMP.Management.Command.Commands;
using EMP.Management.Domain.Models.Employee.Aggregate;
using EMP.Management.Domain.Models.Employee.ValueObject;
using EMP.Management.Infrastructure.Interface.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EMP.Management.Command.CommandHandlers
{
    [Service]
    public class UpdateEmployeeCommandHandler : ICommandHandler<UpdateEmployeeCommand, string>
    {
        private readonly IEmployeeUnitOfWork _employeeUnitOfWork;
        public UpdateEmployeeCommandHandler(IEmployeeUnitOfWork employeeUnitOfWork)
        {
            _employeeUnitOfWork = employeeUnitOfWork;
        }

        public async Task<Result<string>> HandleAsync(UpdateEmployeeCommand commandUpdate)
        {

            var countryCode = CountryCode.Create(commandUpdate.CountryCode);
            var employeeId = EmployeeId.Create(commandUpdate.EmployeeId);
            var employee = GetEmployeeDomain(commandUpdate, employeeId, countryCode);
            var fetchData = await _employeeUnitOfWork.EmployeeRepository.GetByEmployeeIdAsync(employeeId, countryCode);

            if (fetchData.HasValue)
            {
                if (fetchData.Value.Count > 0)
                {
                    return await UpdateEmployee(employee);
                }
                else
                {
                    //validation message update/insert
                }
            }

            return Result.Failure<string>("Fail");
        }

        private async Task<Result<string>> UpdateEmployee(Employee employee)
        {
            try
            {
                await _employeeUnitOfWork.BeginTransactionAsync();
                await _employeeUnitOfWork.EmployeeRepository.AddOrUpdateEmployee(employee);
                await _employeeUnitOfWork.CommitAsync();
                return Result.Success<string>("Success");
            }
            catch (Exception ex)
            {
                await _employeeUnitOfWork.RollBackAsync();
                throw;
            }
        }

        private Domain.Models.Employee.Aggregate.Employee GetEmployeeDomain(UpdateEmployeeCommand commandUpdate, EmployeeId employeeId, CountryCode countryCode)
        {
            var email = Email.Create(commandUpdate.Email);
            var bankDetail = BankDetail.Create(commandUpdate.BankName, commandUpdate.AccountNumber);
            var address = Address.Create(commandUpdate.Address1, commandUpdate.Address2, commandUpdate.Address3, commandUpdate.LandMark);

            var employee = Domain.Models.Employee.Aggregate.Employee.Create(employeeId, countryCode);
            employee.SetEmployeeInfo(commandUpdate.FirstName, commandUpdate.Lastname);
            employee.SetAddress(address);
            employee.SetBankDetails(bankDetail);
            employee.SetEmail(email);

            return employee;
        }
    }
}

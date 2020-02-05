using ARI.EVOS.Dealer.Domain.Models.Dealer.Entity;
using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using Chassis.Command.Interfaces;
using Chassis.RegisterServices.Attributes;
using CSharpFunctionalExtensions;
using EMP.Management.Command.Commands;
using EMP.Management.Command.Interface;
using EMP.Management.Domain.Models.Employee.Aggregate;
using EMP.Management.Domain.Models.Employee.ValueObject;
using EMP.Management.Infrastructure.Interface.Repositories;
using System;
using System.Threading.Tasks;

namespace EMP.Management.Command.CommandHandlers
{
    [Service]
    public class AddEmployeeCommandHandler : ICommandHandler<AddEmployeeCommand, string>
    {
        private readonly IEmployeeValidation _employeeValidation;
        private readonly IEmployeeUnitOfWork _employeeUnitOfWork;

        public AddEmployeeCommandHandler(IEmployeeUnitOfWork employeeUnitOfWork, IEmployeeValidation employeeValidation)
        {
            _employeeUnitOfWork = employeeUnitOfWork;
            _employeeValidation = employeeValidation;
        }

        public async Task<Result<string>> HandleAsync(AddEmployeeCommand commandAdd)
        {
            if (_employeeValidation.IsValid(commandAdd))
            {
                var employeeId = EmployeeId.Create(commandAdd.EmployeeId);
                var countryCode = CountryCode.Create(commandAdd.CountryCode);
                var employeeDetail = GetEmployeeDomain(commandAdd, employeeId, countryCode);

                var fetchData = await _employeeUnitOfWork.EmployeeRepository.GetByEmployeeIdAsync(employeeId, countryCode);
                if (fetchData.HasValue)
                {
                    if (fetchData.Value.Count > 0)
                    {
                        ErrorMessage(CommandConstant.EmployeeExistValidation);
                    }
                    else
                    {
                        return await InsertEmployee(employeeDetail);
                    }
                }
            }

            return Result.Failure<string>("Fail");
        }

        private async Task<Result<string>> InsertEmployee(Employee employeeDetail)
        {
            try
            {
                await _employeeUnitOfWork.BeginTransactionAsync();
                await _employeeUnitOfWork.EmployeeRepository.AddOrUpdateEmployee(employeeDetail);
                await _employeeUnitOfWork.CommitAsync();
                return Result.Success<string>("Success");
            }
            catch (Exception ex)
            {
                await _employeeUnitOfWork.RollBackAsync();
                throw;
            }
        }

        private static void ErrorMessage(string errMessage)
        {
            throw new ArgumentException(errMessage);
        }

        private Domain.Models.Employee.Aggregate.Employee GetEmployeeDomain(AddEmployeeCommand commandAdd, EmployeeId employeeId, CountryCode countryCode)
        {
            var email = Email.Create(commandAdd.Email);
            var bankDetail = BankDetail.Create(commandAdd.BankName, commandAdd.AccountNumber);
            var address = Address.Create(commandAdd.Address1, commandAdd.Address2, commandAdd.Address3, commandAdd.LandMark);

            var employee = Domain.Models.Employee.Aggregate.Employee.Create(employeeId, countryCode);
            employee.SetEmployeeInfo(commandAdd.FirstName, commandAdd.Lastname);
            employee.SetAddress(address);
            employee.SetBankDetails(bankDetail);
            employee.SetEmail(email);

            return employee;
        }
    }
}

using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using Chassis.Command.Interfaces;
using Chassis.RegisterServices.Attributes;
using CSharpFunctionalExtensions;
using EMP.Management.Command.Commands;
using EMP.Management.Domain.Models.Employee.ValueObject;
using EMP.Management.Infrastructure.Interface.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EMP.Management.Command.CommandHandlers
{
    [Service]
    public class DeleteEmployeeCommandHandler : ICommandHandler<DeleteEmployeeCommand, string>
    {
        private readonly IEmployeeUnitOfWork _employeeUnitOfWork;

        public DeleteEmployeeCommandHandler(IEmployeeUnitOfWork employeeUnitOfWork)
        {
            _employeeUnitOfWork = employeeUnitOfWork;
        }

        public async Task<Result<string>> HandleAsync(DeleteEmployeeCommand commandDel)
        {
            var countryCode = CountryCode.Create(commandDel.CountryCode);
            var employeeId = EmployeeId.Create(commandDel.EmployeeId);

            var fetchData = await _employeeUnitOfWork.EmployeeRepository.GetByEmployeeIdAsync(employeeId, countryCode);
            if (fetchData.HasValue && fetchData.Value.Count > 0)
            {
                var resultDealer = fetchData.Value.ToList().FirstOrDefault();

                if (resultDealer != null) return await DeleteEmployee(resultDealer);
            }
            return Result.Failure<string>("Fail");
        }

        private async Task<Result<string>> DeleteEmployee(Domain.Models.Employee.Aggregate.Employee employee)
        {
            try
            {
                await _employeeUnitOfWork.BeginTransactionAsync();
                await _employeeUnitOfWork.EmployeeRepository.DeleteEmployee(employee);
                await _employeeUnitOfWork.CommitAsync();
                return Result.Success<string>("Success");
            }
            catch (Exception ex)
            {
                await _employeeUnitOfWork.RollBackAsync();
                throw;
            }
        }
    }
}

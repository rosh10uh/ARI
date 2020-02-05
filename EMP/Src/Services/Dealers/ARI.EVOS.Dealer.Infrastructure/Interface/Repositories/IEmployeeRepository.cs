using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using CSharpFunctionalExtensions;
using EMP.Management.Domain.Models.Employee.ValueObject;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMP.Management.Infrastructure.Interface.Repositories
{
    public interface IEmployeeRepository
    {
        Task AddOrUpdateEmployee(Domain.Models.Employee.Aggregate.Employee employee);

        Task DeleteEmployee(Domain.Models.Employee.Aggregate.Employee employee);

        Task<IQueryable<Domain.Models.Employee.Aggregate.Employee>> GetAllAsync();

        Task<Maybe<List<Domain.Models.Employee.Aggregate.Employee>>> GetByEmployeeIdAsync(EmployeeId employeeId, CountryCode countryCode);
    }
}

using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using Chassis.Domain;
using Chassis.Repository;
using CSharpFunctionalExtensions;
using EMP.Management.Domain.Models.Employee.ValueObject;
using EMP.Management.Infrastructure.Interface.Repositories;
using EMP.Management.Infrastructure.Specification;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMP.Management.Infrastructure.Repositories
{
    public class EmployeeRepository : DomainRepository<Domain.Models.Employee.Aggregate.Employee, int>, IEmployeeRepository
    {
        public EmployeeRepository(IContext context) : base(context)
        {

        }

        public async Task AddOrUpdateEmployee(Domain.Models.Employee.Aggregate.Employee employee)
        {
            await base.SaveOrUpdateAsync(employee);
        }

        public override async Task<IQueryable<Domain.Models.Employee.Aggregate.Employee>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<Maybe<List<Domain.Models.Employee.Aggregate.Employee>>> GetByEmployeeIdAsync(EmployeeId employeeId, CountryCode countryCode)
        {
            // Set fetch custom criteria using specification 
            var employeeSpecification = new EmployeeIdSpecification(employeeId, countryCode);

            // Get employee Detail using specified criteria in specification 
            return await base.GetAllAsync(employeeSpecification);
        }

        public async Task DeleteEmployee(Domain.Models.Employee.Aggregate.Employee employee)
        {
            await base.DeleteAsync(employee);
        }
    }
}

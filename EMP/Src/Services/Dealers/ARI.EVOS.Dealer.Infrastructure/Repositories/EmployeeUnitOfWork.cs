using Chassis.Repository;
using EMP.Management.Infrastructure.Interface.Repositories;

namespace EMP.Management.Infrastructure.Repositories
{
    public class EmployeeUnitOfWork : UnitOfWork, IEmployeeUnitOfWork
    {
        private readonly IContext _context;
        public EmployeeUnitOfWork(IContext context) : base(context)
        {
            _context = context;
        }
        public IEmployeeRepository EmployeeRepository => new EmployeeRepository(_context);
    }
}

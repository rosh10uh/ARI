using Chassis.Repository;

namespace EMP.Management.Infrastructure.Interface.Repositories
{
    public interface IEmployeeUnitOfWork : IUnitOfWork
    {
        IEmployeeRepository EmployeeRepository { get; }
    }
}

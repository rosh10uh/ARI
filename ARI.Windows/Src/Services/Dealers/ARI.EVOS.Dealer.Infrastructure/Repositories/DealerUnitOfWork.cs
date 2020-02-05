using ARI.EVOS.Dealer.Infrastructure.Interface.Repositories;
using Chassis.Repository;

namespace ARI.EVOS.Dealer.Infrastructure.Repositories
{
    /// <summary>
    /// Get access to Dealer repository.
    /// </summary>
    public class DealerUnitOfWork : UnitOfWork, IDealerUnitOfWork
    {
        private readonly IContext _context;
        public DealerUnitOfWork(IContext context) : base(context)
        {
            _context = context;
        }

        public IDealerRepository DealerRepository => new DealerRepository(_context);

        public IGetReadyRepository GetReadyRepository => new GetReadyRepository(_context);

    }
}

using ARI.EVOS.Dealer.Infrastructure.Interface.Repositories;
using Chassis.RegisterServices.Attributes;
using Chassis.Repository;

namespace ARI.EVOS.Dealer.Infrastructure.Repositories
{
    /// <summary>
    /// Get access to ContactEmail repository.
    /// </summary>
    public class ContactEmailUnitOfWork : UnitOfWork, IContactEmailUnitOfWork
    {
        private readonly IContext _context;
        public ContactEmailUnitOfWork(IContext context) : base(context)
        {
            _context = context;
        }

        public IContactEmailRepository ContactEmailRepository => new ContactEmailRepository(_context);
    }
}

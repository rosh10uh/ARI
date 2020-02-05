using Chassis.Repository;

namespace ARI.EVOS.Dealer.Infrastructure.Interface.Repositories
{
    /// <summary>
    /// This interface is used to get access to Dealer repository.
    /// </summary>
    public interface IDealerUnitOfWork : IUnitOfWork
    {
        IDealerRepository DealerRepository { get;}
        IGetReadyRepository GetReadyRepository { get; }
    }
}

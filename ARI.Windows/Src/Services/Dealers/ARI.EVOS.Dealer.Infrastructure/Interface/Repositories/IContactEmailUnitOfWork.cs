using Chassis.Repository;

namespace ARI.EVOS.Dealer.Infrastructure.Interface.Repositories
{
    /// <summary>
    /// This interface is used to get access to ContactEmail repository.
    /// </summary>
    public interface IContactEmailUnitOfWork: IUnitOfWork
    {
        IContactEmailRepository ContactEmailRepository { get; }
    }
}

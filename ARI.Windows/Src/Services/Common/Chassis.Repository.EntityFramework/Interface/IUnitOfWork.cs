using System.Data;
using System.Threading.Tasks;

namespace Chassis.Repository.EntityFramework.Interface
{
    /// <summary>
    /// Interface for unit of work
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// For multiple transaction, first BeginTransaction will be used.
        /// </summary>
        /// <param name="isolationLevel">Specifies the transaction locking behavior for the connection.</param>
        Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

        /// <summary>
        /// Commit changes to the database.
        /// </summary>
        Task CommitAsync();

        /// <summary>
        /// Changes rollback from the database is any exception raised while transaction.
        /// </summary>
        Task RollBackAsync();

    }
}

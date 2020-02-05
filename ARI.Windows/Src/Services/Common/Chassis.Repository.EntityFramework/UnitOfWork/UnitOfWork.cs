using System.Data;
using System.Threading.Tasks;
using Chassis.Repository.EntityFramework.Interface;
using Microsoft.EntityFrameworkCore;

namespace Chassis.Repository.EntityFramework.UnitOfWork
{
    /// <summary>
    /// Abstract class for unit of work
    /// </summary>
    public abstract class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        private ITransaction _transaction;

        /// <summary>
        /// Injects IContext and binds with DbContext.
        /// </summary>
        /// <param name="context">Pass the context.</param>
        protected UnitOfWork(IContext context)
        {
            _dbContext = context.DbContext;
        }


        /// <summary>
        /// Asynchronously starts a new transaction with a given System.Data.IsolationLevel.
        /// </summary>
        /// <param name="isolationLevel">Specifies the transaction locking behavior for the connection.</param>
        public async Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            _transaction = await EntityFramworkTransaction.CreateTransactionAsync(_dbContext, isolationLevel).ConfigureAwait(false);
        }


        /// <summary>
        /// For committing the transaction.
        /// </summary>
        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            await _transaction.CommitAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// For Transaction Rollback.
        /// </summary>
        public Task RollBackAsync()
        {
            return _transaction.RollBackAsync();
        }
    }
}

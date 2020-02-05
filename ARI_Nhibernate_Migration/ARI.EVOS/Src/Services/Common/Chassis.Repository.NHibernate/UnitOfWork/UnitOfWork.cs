using NHibernate;
using Chassis.Repository.NHibernate;
using System.Data;
using System.Threading.Tasks;

namespace Chassis.Repository
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        private readonly ISession _session;
        private ITransaction _transaction;

        /// <summary>
        /// Injects IContext.
        /// </summary>
        /// <param name="context">Pass the context.</param>
        protected UnitOfWork(IContext context)
        {
            _session = context.Session;
        }


        /// <summary>
        /// For multiple transaction.
        /// </summary>
        /// <param name="isolationLevel">Specifies the transaction locking behavior for the connection.</param>
        public Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            _transaction = new NHibernateTransaction(_session, isolationLevel);
            return Task.CompletedTask;
        }

        /// <summary>
        /// For committing the transaction.
        /// </summary>
        public async Task CommitAsync()
        {
            await _transaction.CommitAsync();
        }

        /// <summary>
        /// For Transaction Rollback.
        /// </summary>
        public async Task RollBackAsync()
        {
            await _transaction.RollBackAsync();
        }
    }
}

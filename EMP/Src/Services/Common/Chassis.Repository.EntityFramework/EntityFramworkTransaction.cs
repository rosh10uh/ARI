using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data;
using System.Threading.Tasks;
using Chassis.Repository.EntityFramework.Interface;

namespace Chassis.Repository.EntityFramework
{
    /// <summary>
    /// This class use to transaction management
    /// </summary>
    internal class EntityFramworkTransaction : ITransaction
    {
        /// <summary>
        /// A transaction against the database.
        /// </summary>
        private IDbContextTransaction _dbContextTransaction;

        /// <summary>
        /// Responsible for begin database transaction
        /// </summary>
        /// <param name="dbContext"> A DbContext instance represents a session with the database and can be used to
        /// query and save instances of your entities.</param>
        /// <param name="isolationLevel">Specifies the transaction locking behavior for the connection.</param>
        internal static async Task<ITransaction> CreateTransactionAsync(DbContext dbContext, IsolationLevel isolationLevel)
        {
            EntityFramworkTransaction transaction = new EntityFramworkTransaction();
            await transaction.BeginTransactionAsync(dbContext, isolationLevel);
            return transaction;
        }

        /// <summary>
        /// Responsible for begin database transaction
        /// </summary>
        /// <param name="dbContext"> A DbContext instance represents a session with the database and can be used to
        /// query and save instances of your entities.</param>
        /// <param name="isolationLevel">Specifies the transaction locking behavior for the connection.</param>
        private async Task BeginTransactionAsync(DbContext dbContext, IsolationLevel isolationLevel)
        {
            _dbContextTransaction = await dbContext.Database.BeginTransactionAsync(isolationLevel);
        }

        /// <summary>
        /// To commit current transaction
        /// </summary>
        public Task CommitAsync()
        {
            _dbContextTransaction.Commit();
            return Task.CompletedTask;
        }

        /// <summary>
        /// To roll back current transaction
        /// </summary>
        public Task RollBackAsync()
        {
            _dbContextTransaction.Rollback();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Dispose the current transaction object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose the current transaction object
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContextTransaction.Dispose();
            }
        }
    }
}

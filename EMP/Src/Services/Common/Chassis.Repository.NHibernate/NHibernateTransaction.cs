using nhibernate = NHibernate;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Chassis.Repository.NHibernate
{
    /// <summary>
    /// It is Use for NHibernate Transactions 
    /// </summary>
    internal class NHibernateTransaction : ITransaction
    {
        private readonly nhibernate.ITransaction _transaction;

        /// <summary>
        /// Initializes NHibernate Transaction
        /// </summary>
        /// <param name="session">ISession object</param>
        /// <param name="isolationLevel"> object of Isolation level for the transaction</param>
        public NHibernateTransaction(nhibernate.ISession session, IsolationLevel isolationLevel)
        {
            _transaction = session.BeginTransaction(isolationLevel);
        }

        /// <summary>
        /// It is Use for Commit Transactions 
        /// </summary>
        public Task CommitAsync()
        {
            return _transaction.CommitAsync();
        }

        /// <summary>
        /// It is Use for RollBack Transactions 
        /// </summary>
        public Task RollBackAsync()
        {
            return _transaction.RollbackAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// It is Use for Dispose the session base on boolean value
        /// </summary>
        /// <param name="disposing">true/false</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _transaction.Dispose();
            }
        }
    }
}

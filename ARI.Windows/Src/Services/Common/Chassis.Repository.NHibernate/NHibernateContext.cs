using NHibernate;
using System;

namespace Chassis.Repository.NHibernate
{
    /// <summary>
    /// It is Use for NHibernate Context 
    /// </summary>
    internal class NHibernateContext : IContext
    {
        public ISession Session { get; }

        /// <summary>
        /// Initializes NHibernate Context
        /// </summary>
        /// <param name="session">ISession object</param>
        internal NHibernateContext(ISession session)
        {
            Session = session;
        }

        /// <summary>
        /// It is Use for Dispose the session
        /// </summary>
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
                Session.Dispose();
            }
        }
    }
}

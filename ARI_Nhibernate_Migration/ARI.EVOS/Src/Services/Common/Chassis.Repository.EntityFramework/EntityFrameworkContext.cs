using Microsoft.EntityFrameworkCore;
using System;
using Chassis.Repository.EntityFramework.Interface;

namespace Chassis.Repository.EntityFramework
{
    /// <summary>
    /// This class is core implementation for entity framework
    /// </summary>
    internal class EntityFrameworkContext : IContext
    {
        public DbContext DbContext { get; }

        /// <summary>
        /// Constructor with DbContext parameter
        /// </summary>
        /// <param name="dbContext">DbContext</param>
        internal EntityFrameworkContext(DbContext dbContext)
        {
            DbContext = dbContext;
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
                DbContext.Dispose();
            }
        }

    }
}

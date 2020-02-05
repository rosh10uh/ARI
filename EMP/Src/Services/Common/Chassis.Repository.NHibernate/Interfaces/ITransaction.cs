using System;
using System.Threading.Tasks;

namespace Chassis.Repository
{
    internal interface ITransaction : IDisposable
    {
        /// <summary>
        /// To commit transactional data in database.
        /// </summary>
        Task CommitAsync();

        /// <summary>
        /// If any error raised then the transaction would be rollback.
        /// </summary>
        Task RollBackAsync();
    }
}

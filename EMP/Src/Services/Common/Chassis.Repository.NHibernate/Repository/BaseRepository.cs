using Chassis.Repository.Interfaces;
using NHibernate;
using System.Threading.Tasks;

namespace Chassis.Repository
{
    public class BaseRepository<TEntity> : BaseReadOnlyRepository<TEntity>, IRepository<TEntity> where TEntity : class
    {
        private readonly ISession _session;

        /// <summary>
        /// Injects IContext.
        /// </summary>
        /// <param name="context">Pass the context.</param>
        public BaseRepository(IContext context) : base(context)
        {
            _session = context.Session;
        }

        /// <summary>
        /// It is Use for save data
        /// </summary>
        /// <typeparam name="TEntity">class type entity</typeparam>
        /// <param name="entity">class type object</param>
        public virtual Task SaveOrUpdateAsync(TEntity entity)
        {
            return _session.SaveOrUpdateAsync(entity);
        }

        /// <summary>
        /// It is Use for delete data
        /// </summary>
        /// <typeparam name="TEntity">class type entity</typeparam>
        /// <param name="entity">class type object</param>
        public virtual Task DeleteAsync(TEntity entity)
        {
            return _session.DeleteAsync(entity);
        }

        /// <summary>
        /// Force the ISession to flush
        /// </summary>
        public virtual Task FlushAsync()
        {
            return _session.FlushAsync();
        }
    }
}

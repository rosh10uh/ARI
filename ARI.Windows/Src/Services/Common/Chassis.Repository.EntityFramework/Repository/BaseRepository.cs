using System.Threading.Tasks;
using Chassis.Repository.EntityFramework.Interface;
using Chassis.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chassis.Repository.EntityFramework.Repository
{
    /// <summary>
    /// Repository Entity Framework for data manipulation operation.
    /// </summary>
    public class BaseRepository<TEntity> : BaseReadOnlyRepository<TEntity>, IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// For static binding with context this property has been created.
        /// </summary>
        private readonly DbContext _dbContext;

        /// <summary>
        /// Injects IContext and also called BaseReadOnlyRepository constructor.
        /// </summary>
        /// <param name="context">For static binding context has been passed.</param>
        public BaseRepository(IContext context) : base(context)
        {
            _dbContext = context.DbContext;
        }

        /// <summary>
        /// New or existing record will be saved.
        /// </summary>
        /// <param name="entity">Type of class will be passed.</param>
        public virtual Task SaveOrUpdateAsync(TEntity entity)
        {

            Task task = null;
            var entry = _dbContext.Entry(entity);
            switch (entry.State)
            {
                case EntityState.Detached:
                case EntityState.Added:
                    task = _dbContext.AddAsync(entity);
                    break;
                case EntityState.Modified:
                    _dbContext.Update(entity);
                    break;
            }
            return task ?? Task.CompletedTask;
        }

        /// <summary>
        /// Begins tracking the given entity in the Microsoft.EntityFrameworkCore.EntityState.Deleted
        /// state such that it will be removed from the database when Microsoft.EntityFrameworkCore.DbContext.SaveChanges
        /// is called.
        /// </summary>
        /// <param name="entity">The entity to remove</param>
        /// <returns></returns>
        public virtual Task DeleteAsync(TEntity entity)
        {
            var result = _dbContext.Remove(entity);
            return Task.FromResult(result);
        }

        /// <summary>
        /// Changes save to Database.
        /// </summary>
        public virtual async Task FlushAsync()
        {
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

    }
}

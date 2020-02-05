using System.Threading.Tasks;

namespace Chassis.Repository.Interfaces
{
    /// <summary>
    /// Interface of BaseRepository of type TEntity.
    /// This interface is used for saving, updating or deleting the records.
    /// </summary>
    /// <typeparam name="TEntity">TEntity will take object type only.</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// This method is used for saving or updating the records.
        /// </summary>
        /// <param name="entity">TEntity type parameter will be passed here.</param>
        Task SaveOrUpdateAsync(TEntity entity);

        /// <summary>
        /// This method is used for deleting the records.
        /// </summary>
        /// <param name="entity">TEntity type parameter will be passed here.</param>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// SaveChanges to database.
        /// </summary>
        Task FlushAsync();
    }
}

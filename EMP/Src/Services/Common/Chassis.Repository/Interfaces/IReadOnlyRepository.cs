using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chassis.Repository.Specification;
using CSharpFunctionalExtensions;

namespace Chassis.Repository.Interfaces
{
    /// <summary>
    /// Interface for ReadOnly Repository of type TEntity.
    /// For getting records this repository is used.
    /// </summary>
    /// <typeparam name="TEntity">TEntity will take object only.</typeparam>
    public interface IReadOnlyRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// GetById method which is of type TEntity it will take object of particular type.
        /// </summary>
        /// <typeparam name="TPrimaryKey">Primary key type of the Entity.</typeparam>
        /// <param name="id">Pass id of type primary key.</param>
        /// <returns>The type of object which we pass.</returns>
        /// Working
        Task<Maybe<TEntity>> GetByIdAsync<TPrimaryKey>(TPrimaryKey id);

        /// <summary>
        /// Get list of records for TEntity type.
        /// </summary>
        /// <returns>Multiple Records.</returns>
        Task<IQueryable<TEntity>> GetAllAsync();

        /// <summary>
        /// Get records whose criteria will be matched with the Specification using type TEntity.
        /// </summary>
        /// <typeparam name="TEntity">Take TEntity type.</typeparam>
        /// <param name="specification">specification parameter will be passed with particular TEntity type.</param>
        /// <returns>Record whose criteria is matched.</returns>
        Task<Maybe<List<TEntity>>> GetAllAsync(Specification<TEntity> specification);
    }
}

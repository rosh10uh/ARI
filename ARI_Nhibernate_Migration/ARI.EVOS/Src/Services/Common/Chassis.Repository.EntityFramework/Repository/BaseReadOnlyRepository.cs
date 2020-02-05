using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chassis.Repository.EntityFramework.Interface;
using Chassis.Repository.Interfaces;
using Chassis.Repository.Specification;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Chassis.Repository.EntityFramework.Repository
{
    /// <summary>
    /// For reading the data this repository will be used.
    /// </summary>
    public class BaseReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// For static binding property has been created.
        /// </summary>
        private readonly DbContext _dbContext;

        /// <summary>
        /// Injects IContext and do static binding with read only repository.
        /// </summary>
        /// <param name="context"></param>
        public BaseReadOnlyRepository(IContext context)
        {
            _dbContext = context.DbContext;
        }

        /// <summary>
        /// To get All the records.
        /// Asynchronous method of type Maybe which may or may not returns the records.
        /// </summary>
        /// <returns>Multiple records.</returns>
        public async virtual Task<IQueryable<TEntity>> GetAllAsync()
        {
            var result = _dbContext.Set<TEntity>();
            return await Task.FromResult(result).ConfigureAwait(false);
        }

        /// <summary>
        /// Get records which is matched with the specification criteria.
        /// Asynchronous method of type Maybe which may or may not returns the records.
        /// </summary>
        /// <param name="specification">Pass specification</param>
        /// <returns>Record whose criteria is matched.</returns>
        public async virtual Task<Maybe<List<TEntity>>> GetAllAsync(Specification<TEntity> specification)
        {
            var result = _dbContext.Set<TEntity>().Where(specification.ToExpression()).ToList();
            return await Task.FromResult(result).ConfigureAwait(false);
        }

        /// <summary>
        /// Get record from id.
        /// Asynchronous method which may return the records or may not.
        /// </summary>
        /// <typeparam name="TPrimaryKey">Key type passed.</typeparam>
        /// <param name="id">Id will be passed</param>
        /// <returns>Single record from the specified Id.</returns>
        public async virtual Task<Maybe<TEntity>> GetByIdAsync<TPrimaryKey>(TPrimaryKey id)
        {
            return await _dbContext.FindAsync<TEntity>(id).ConfigureAwait(false);
        }
    }
}

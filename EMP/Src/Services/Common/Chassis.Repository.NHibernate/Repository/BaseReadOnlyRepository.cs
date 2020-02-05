using CSharpFunctionalExtensions;
using NHibernate;
using NHibernate.Linq;
using Chassis.Repository.Specification;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chassis.Repository.Interfaces;

namespace Chassis.Repository
{
    /// <summary>
    /// For reading the data this repository will be used.
    /// </summary>
    /// <typeparam name="TEntity">It will take type as a class</typeparam>
    public class BaseReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class
    {
        private readonly ISession _session;

        /// <summary>
        /// Injects IContext.
        /// </summary>
        /// <param name="context">Pass the context.</param>
        public BaseReadOnlyRepository(IContext context)
        {
            _session = context.Session;
        }

        /// <summary>
        /// It is Use for Get All data 
        /// </summary>
        /// <typeparam name="TEntity">class type entity</typeparam>
        /// <returns>executes a select query on server-side with all filters and returns list of class type entity data</returns>
        public async virtual Task<IQueryable<TEntity>> GetAllAsync()
        {
            var result = _session.Query<TEntity>();
            return await Task.FromResult(result).ConfigureAwait(false);
        }

        /// <summary>
        /// It is Use for Get All data base on specification
        /// </summary>
        /// <typeparam name="TEntity">class type entity</typeparam>
        /// <param name="specification">Specification object</param>
        /// <returns>read only list of class type entity data</returns>
        public async virtual Task<Maybe<List<TEntity>>> GetAllAsync(Specification<TEntity> specification)
        {
            //return await _session.Query<TEntity>().Where(specification.ToExpression()).AsQueryable().ToListAsync().ConfigureAwait(false);
            var result = _session.Query<TEntity>().Where(specification.ToExpression()).AsQueryable().ToList();
            return await Task.FromResult(result).ConfigureAwait(false);
        }

        /// <summary>
        /// It is Use for Get data base on Id
        /// </summary>
        /// <typeparam name="TPrimaryKey">PrimaryKey type will be taken</typeparam>
        /// <typeparam name="TEntity">class type entity</typeparam>
        /// <param name="id">PrimaryKey type</param>
        /// <returns>class type entity</returns>
        public async virtual Task<Maybe<TEntity>> GetByIdAsync<TPrimaryKey>(TPrimaryKey id)
        {
            return await _session.GetAsync<TEntity>(id).ConfigureAwait(false);
        }
    }
}

using Chassis.Domain.Aggregate;
using Chassis.Repository.EntityFramework.Interface;
using Chassis.Repository.EntityFramework.Repository;

namespace Chassis.Domain.EntityFramework
{
    /// <summary>
    /// Generic domain repository which extend base repository
    /// </summary>
    /// <typeparam name="TEntity">TEntity type will take a particular class in it</typeparam>
    /// <typeparam name="TPrimaryKey">PrimaryKey type will be taken</typeparam>
    public abstract class DomainRepository<TEntity, TPrimaryKey> : BaseRepository<TEntity>
                                                   where TEntity : AggregateRoot<TPrimaryKey>
    {
        protected DomainRepository(IContext context) : base(context)
        {

        }
    }
}

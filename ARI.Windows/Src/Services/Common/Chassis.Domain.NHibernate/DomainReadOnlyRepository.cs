using Chassis.Domain.Aggregate;
using Chassis.Repository;

namespace Chassis.Domain
{
    /// <summary>
    /// Generic domain read only repository which extend base read only repository 
    /// </summary>
    /// <typeparam name="TEntity">TEntity type will take a particular class in it</typeparam>
    /// <typeparam name="TPrimaryKey">PrimaryKey type will be taken</typeparam>
    public abstract class DomainReadOnlyRepository<TEntity, TPrimaryKey> : BaseReadOnlyRepository<TEntity>
                                                           where TEntity : Entity<TPrimaryKey>
    {
        protected DomainReadOnlyRepository(IContext context) : base(context)
        {

        }
    }
}

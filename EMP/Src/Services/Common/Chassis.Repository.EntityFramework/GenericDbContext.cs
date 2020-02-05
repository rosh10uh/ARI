using Microsoft.EntityFrameworkCore;

namespace Chassis.Repository.EntityFramework
{
    /// <summary>
    /// This delegate further configure the model that was discovered by convention
    /// from the entity types exposed
    /// </summary>
    /// <param name="modelBuilder"></param>
    public delegate void EfModelBuilder(ModelBuilder modelBuilder);

    /// <summary>
    /// This class is use for provide generic dbContext
    /// </summary>
    public class GenericDbContext : DbContext
    {
        /// <summary>
        /// Delegate event invoke on OnModelCreating method for configure the model that was discovered by convention
        /// from the entity types exposed
        /// </summary>
        public event EfModelBuilder OnEfModelBuilder;

        /// <summary>
        /// Constructor with parameter DbContextOptions with GenericDbContext type
        /// </summary>
        /// <param name="option">DbContextOptions</param>
        public GenericDbContext(DbContextOptions option) : base(option)
        {

        }

        /// <summary>
        /// Discovered by convention from the entity types exposed in Microsoft.EntityFrameworkCore.DbSet`1 properties on your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnEfModelBuilder?.Invoke(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

    }
}

namespace Chassis.Repository.Interfaces
{
    /// <summary>
    /// Specification interface which will be used by EF and NHibernate to implement this pattern.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface ISpecification<TEntity>
    {
        /// <summary>
        /// This property is used for satisfying the expression.
        /// Expression is a class which represents a strongly typed lambda expression as a data structure in the form of an expression tree.
        /// </summary> 
        bool IsSatisfiedBy(TEntity entity);
    }
}

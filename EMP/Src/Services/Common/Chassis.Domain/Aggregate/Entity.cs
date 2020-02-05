namespace Chassis.Domain.Aggregate
{
    /// <summary>
    /// An abstract class should be overridden in domain model which contains entity
    /// </summary>
    /// <typeparam name="TPrimaryKey">PrimaryKey type will be taken</typeparam>
    public abstract class Entity<TPrimaryKey>
    {
        public virtual TPrimaryKey Id { get;  set; }

        protected virtual object Actual => this;

        /// <summary>
        ///  Compare objects of type entity and return true/false
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>true/false</returns>
        public override bool Equals(object obj)
        {
            var other = obj as Entity<TPrimaryKey>;

            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (Actual.GetType() != other.Actual.GetType())
                return false;

            return Id.Equals(other.Id);
        }

        /// <summary>
        /// Compare objects of type entity with == operator and return true/false
        /// </summary>
        /// <param name="a">Entity<TPrimaryKey></param>
        /// <param name="b">Entity<TPrimaryKey></param>
        /// <returns>true/false</returns>
        public static bool operator ==(Entity<TPrimaryKey> a, Entity<TPrimaryKey> b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        /// <summary>
        /// Compare objects of type entity with != operator and return true/false
        /// </summary>
        /// <param name="a">Entity<TPrimaryKey></param>
        /// <param name="b">Entity<TPrimaryKey></param>
        /// <returns>true/false</returns>
        public static bool operator !=(Entity<TPrimaryKey> a, Entity<TPrimaryKey> b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Get hashcode of id
        /// </summary>
        /// <returns>int</returns>
        public override int GetHashCode()
        {
            return (Actual.GetType().ToString() + Id).GetHashCode();
        }
    }
}

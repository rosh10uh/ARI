namespace Chassis.Domain.Aggregate
{
    /// <summary>
    /// An abstract class is overridden in domain model which contains value object
    /// </summary>
    /// <typeparam name="T">T is type of class</typeparam>
    public abstract class ValueObject<T> where T : ValueObject<T>
    {
        /// <summary>
        /// Compare objects of type value object and return true/false
        /// </summary>
        /// <param name="obj">Object of type ValueObject</param>
        /// <returns>true/false</returns>
        public override bool Equals(object obj)
        {
            var valueObject = obj as T;

            if (ReferenceEquals(valueObject, null))
                return false;

            return EqualsCore(valueObject);
        }

        /// <summary>
        /// An abstract method should be overridden in derived class
        /// </summary>
        /// <param name="other">T is type of class</param>
        /// <returns>true/false</returns>
        protected abstract bool EqualsCore(T other);

        /// <summary>
        /// Get hashcode and it is called GetHasCodeCore method which is overridden in derived class
        /// </summary>
        /// <returns>int</returns>
        public override int GetHashCode()
        {
            return GetHashCodeCore();
        }

        /// <summary>
        /// An abstract method should be overridden in derived class
        /// </summary>
        /// <returns>int</returns>
        protected abstract int GetHashCodeCore();

        /// <summary>
        /// Compare object of type value object
        /// </summary>
        /// <param name="a">ValueObject<T></param>
        /// <param name="b">ValueObject<T></param>
        /// <returns>true/false</returns>
        public static bool operator ==(ValueObject<T> a, ValueObject<T> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        /// <summary>
        /// Compare object of type value object
        /// </summary>
        /// <param name="a">ValueObject<T></param>
        /// <param name="b">ValueObject<T></param>
        /// <returns>true/false</returns>
        public static bool operator !=(ValueObject<T> a, ValueObject<T> b)
        {
            return !(a == b);
        }
    }
}

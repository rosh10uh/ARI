using Chassis.Domain.Interface;
using System.Collections.Generic;

namespace Chassis.Domain.Aggregate
{
    /// <summary>
    /// An abstract class should be overridden in domain model which contains aggregate root
    /// </summary>
    /// <typeparam name="TPrimaryKey">PrimaryKey type will be taken</typeparam>
    public abstract class AggregateRoot<TPrimaryKey> : Entity<TPrimaryKey>
    {
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

        public virtual IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;

        /// <summary>
        /// Add Domain events
        /// </summary>
        /// <param name="newEvent">IDomainEvent interface</param>
        protected virtual void AddDomainEvent(IDomainEvent newEvent)
        {
            _domainEvents.Add(newEvent);
        }

        /// <summary>
        /// Clear domain events
        /// </summary>
        public virtual void ClearEvents()
        {
            _domainEvents.Clear();
        }
    }
}

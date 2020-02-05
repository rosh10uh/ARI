using Chassis.Domain.Aggregate;

namespace ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject
{
    /// <summary>
    /// This class is used to store dealer id value
    /// </summary>
    public class DealerId : ValueObject<DealerId>
    {
        public virtual string Id { get; protected set; }

        protected DealerId()
        {
        }

        private DealerId(string id)
        {
            Id = id;
        }

        public static DealerId Create(string id)
        {
            return new DealerId(id);
        }

        protected override bool EqualsCore(DealerId dealerId)
        {
            return Id == dealerId.Id;
        }

        protected override int GetHashCodeCore()
        {
            return 13 * Id.Length;
        }
    }
}

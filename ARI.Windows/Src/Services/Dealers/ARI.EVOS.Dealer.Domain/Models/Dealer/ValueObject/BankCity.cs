using Chassis.Domain.Aggregate;

namespace ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject
{
    /// <summary>
    /// This class is used to store bank city value
    /// </summary>
    public class BankCity : ValueObject<BankCity>
    {
        public virtual string City { get; protected set; }
        public virtual MakeCode MakeCode { get; protected set; }
        public virtual DealerId DealerId { get; protected set; }
        protected BankCity()
        {
        }

        private BankCity(string city, MakeCode makeCode, DealerId dealerId)
        {
            City = city;
            MakeCode = makeCode;
            DealerId = dealerId;
        }

        public static BankCity Create(string bankCity, MakeCode makeCode, DealerId dealerId)
        {
            return new BankCity(bankCity, makeCode, dealerId);
        }

        protected override bool EqualsCore(BankCity other)
        {
            return City == other.City && DealerId == other.DealerId;
        }

        protected override int GetHashCodeCore()
        {
            return base.GetHashCode();
        }
    }
}

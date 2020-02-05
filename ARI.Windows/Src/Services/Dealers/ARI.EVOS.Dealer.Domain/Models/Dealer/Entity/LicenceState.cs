using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using Chassis.Domain.Aggregate;

namespace ARI.EVOS.Dealer.Domain.Models.Dealer.Entity
{
    /// <summary>
    /// This class is used to store dealer licence state details
    /// </summary>
    public class LicenceState : Entity<int>
    {
        public override int Id { get;  set; }
        public DealerId DealerId { get; protected set; }
        public CountryCode CountryCode { get; protected set; }
        public MakeCode MakeCode { get; protected set; }
        public string StateCode { get; protected set; }

        protected LicenceState()
        {
        }

        private LicenceState(DealerId dealerId, CountryCode countryCode, MakeCode makeCode, string stateCode)
        {
            DealerId = dealerId;
            CountryCode = countryCode;
            MakeCode = makeCode;
            StateCode = stateCode;
        }

        public static LicenceState Create(DealerId dealerId, CountryCode countryCode, MakeCode makeCode, string stateCode)
        {
            return new LicenceState(dealerId, countryCode, makeCode, stateCode);
        }
    }
}

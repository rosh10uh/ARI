using ARI.EVOS.Dealer.Domain.SharedKernel;
using Chassis.Domain.Aggregate;
using System;
using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;

namespace ARI.EVOS.Dealer.Domain.Models.Dealer.Entity
{
    /// <summary>
    /// This class is used to store dealer get ready details
    /// </summary>
    public class GetReady : Entity<int>
    {
        public virtual CountryCode CountryCode { get; protected set; }
        public virtual MakeCode MakeCode { get; protected set; }
        public virtual DealerId DealerId { get; protected set; }
        public virtual string GRVehicles { get; protected set; }
        public virtual string ClientId { get; protected set; }
        public virtual decimal? Amount { get; protected set; }
        public virtual EffectiveDate EffectiveDate { get; protected set; }
        public virtual Program? LastProgram { get; protected set; }
        public virtual User LastUser { get; protected set; }
        public virtual DateTime? LastChange { get; protected set; }
        protected GetReady()
        {
        }

        private GetReady(CountryCode countryCode, MakeCode makeCode, DealerId dealerId)
        {
            CountryCode = countryCode;
            MakeCode = makeCode;
            DealerId = dealerId;           
        }

        public static GetReady Create(CountryCode countryCode, MakeCode makeCode, DealerId dealerId)
        {
            return  new GetReady(countryCode, makeCode, dealerId);            
        }
        
        public virtual void Update (CountryCode countryCode, MakeCode makeCode, DealerId dealerId)
        {
            CountryCode = countryCode;
            MakeCode = makeCode;
            DealerId = dealerId;           
        }
        
        public virtual void SetGetReadyInfo(string grVehicle, string clientId, decimal? amount, EffectiveDate effectiveDate,
          Program? lastProgram, User lastUser, DateTime? lastChange)
        {
            GRVehicles = grVehicle;
            ClientId = clientId;
            Amount = amount;
            EffectiveDate = effectiveDate;
            LastProgram = lastProgram;
            LastUser = lastUser;
            LastChange = lastChange;
        }
    }
}

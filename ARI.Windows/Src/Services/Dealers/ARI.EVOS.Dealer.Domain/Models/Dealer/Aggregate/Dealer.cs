using ARI.EVOS.Dealer.Domain.Models.Dealer.Entity;
using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using ARI.EVOS.Dealer.Domain.SharedKernel;
using Chassis.Domain.Aggregate;
using System;

namespace ARI.EVOS.Dealer.Domain.Models.Dealer.Aggregate
{
    /// <summary>
    /// This class is used to store dealer details
    /// </summary>
    public class Dealer : AggregateRoot<int>
    {
        public virtual CountryCode CountryCode { get; protected set; }
        public virtual MakeCode MakeCode { get; protected set; }
        public virtual DealerId DealerId { get; protected set; }
        public virtual Vendor Vendor { get; protected set; }
        public virtual ContactInformation ContactInformation { get; protected set; }
        public virtual Address Address { get; protected set; }
        public virtual string Comment { get; protected set; }

        public virtual Program? CreationProgram { get; protected set; }
        public virtual User CreationUser { get; protected set; }
        public virtual DateTime? CreationDate { get; protected set; }
        public virtual DateTime? LastUsedDate { get; protected set; }
        public virtual Program? LastProgram { get; protected set; }
        public virtual User LastUser { get; protected set; }
        public virtual DateTime? LastChg { get; protected set; }
        public virtual DealerOtherInformation AdditionalInformation { get; protected set; }
        public virtual BankDetail BankDetail { get; protected set; }
        public virtual string KeyDealerClient { get; protected set; }
        public virtual string KeyDealerDiv { get; protected set; }
        public virtual int? RebateVolumeFactory { get; protected set; }
        public virtual int? RebateVolumeStock { get; protected set; }
        public virtual float? UKCommission { get; protected set; }

        protected Dealer()
        {

        }

        private Dealer(CountryCode countryCode, MakeCode makeCode, DealerId dealerId)
        {
            CountryCode = countryCode;
            MakeCode = makeCode;
            DealerId = dealerId;
        }

        public static Dealer Create(CountryCode countryCode, MakeCode makeCode, DealerId dealerId)
        {
            return new Dealer(countryCode, makeCode, dealerId);
        }

        public virtual void SetDealerInfo(string comment, Program creationProgram, DateTime creationDate, DateTime lastUsed, Program lastProgram, DateTime lastChg)

        {
            Comment = comment;
            CreationProgram = creationProgram;
            CreationDate = creationDate;
            LastUsedDate = lastUsed;
            LastProgram = lastProgram;
            LastChg = lastChg;
        }

        public virtual void SetDealerOtherInfo(string keyDealerClient, string keyDealerDiv, int? rebateVolumeFactory, int? rebateVolumeStock, float? uKCommission)
        {
            KeyDealerClient = keyDealerClient;
            KeyDealerDiv = keyDealerDiv;
            RebateVolumeFactory = rebateVolumeFactory;
            RebateVolumeStock = rebateVolumeStock;
            UKCommission = uKCommission;
        }
        public virtual void SetUser(User creationUser, User lastUser)
        {
            CreationUser = creationUser;
            LastUser = lastUser;
        }
        public virtual void SetVendor(Vendor vendor)
        {
            Vendor = vendor;
        }
        public virtual void SetContactInformation(ContactInformation contactInformation)
        {
            ContactInformation = contactInformation;
        }
        public virtual void SetAddress(Address address)
        {
            Address = address;
        }

        public virtual void SetAdditionalInformation(DealerOtherInformation additionalInformation)
        {
            AdditionalInformation = additionalInformation;
        }
        public virtual void SetBankAccount(BankDetail bankDetail)
        {
            BankDetail = bankDetail;
        }
    }
}

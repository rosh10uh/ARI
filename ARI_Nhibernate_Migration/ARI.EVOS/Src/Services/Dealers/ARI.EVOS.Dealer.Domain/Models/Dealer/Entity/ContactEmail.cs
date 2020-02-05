using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using ARI.EVOS.Dealer.Domain.SharedKernel;
using Chassis.Domain.Aggregate;

namespace ARI.EVOS.Dealer.Domain.Models.Dealer.Entity
{
    /// <summary>
    /// This class is used to store Contact Email details
    /// </summary>
    public class ContactEmail : Entity<int>
    {
        public virtual CountryCode CountryCode { get; protected set; }
        public virtual MakeCode MakeCode { get; protected set; }
        public virtual DealerId DealerId { get; protected set; }
        public virtual ContactType ContactType { get; protected set; }
        public virtual string Name { get; protected set; }
        public virtual Email Email { get; protected set; }

        protected ContactEmail()
        {
        }

        private ContactEmail(CountryCode countryCode, MakeCode makeCode, DealerId dealerId, ContactType contactType, string name, Email email)
        {
            CountryCode = countryCode;
            MakeCode = makeCode;
            DealerId = dealerId;
            ContactType = contactType;
            Name = name;
            Email = email;
        }

        public static ContactEmail Create(CountryCode countryCode, MakeCode makeCode, DealerId dealerId, ContactType contactType, string name, Email email)
        {
            return new ContactEmail(countryCode, makeCode, dealerId, contactType, name, email);
        }
        
        public virtual ContactEmail SaveAdditionalEmail(ContactEmail contactEmail)
        {            
            return contactEmail;
        }
        
        public virtual ContactEmail SaveUpdateEmail(ContactEmail contactEmail, string name, Email email)
        {
            contactEmail.Name = name;
            contactEmail.Email = email;            
            return contactEmail;
        }
}
}

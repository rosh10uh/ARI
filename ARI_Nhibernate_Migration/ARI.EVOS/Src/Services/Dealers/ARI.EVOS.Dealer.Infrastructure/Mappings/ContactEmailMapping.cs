using ARI.EVOS.Dealer.Domain.Models.Dealer.Entity;
using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using FluentNHibernate.Mapping;

namespace ARI.EVOS.Dealer.Infrastructure.Mappings
{
    /// <summary>
    /// This class is used for contact email entity domain model mapping
    /// </summary>
    public class ContactEmailMapping : ClassMap<ContactEmail>
    {
        public ContactEmailMapping()
        {
            Table("DEALER_ADDL_CONTACTS");            
            CompositeId()
                .KeyProperty(x => x.ContactType, "CONTACT_TYPE")                
                .KeyReference(x => x.CountryCode, "COUNTRY_CODE")
                .KeyReference(x => x.DealerId, "DEALER_ID")
                .KeyReference(x => x.MakeCode, "MAKE_CODE");
            Map(x => x.Name, "NAME");
            Component(x => x.Email, y =>
            {
                y.Map(p => p.EmailAddress).Column("EMAIL_ADDRESS");
                
            });
        }
    }
}

using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using FluentNHibernate.Mapping;

namespace ARI.EVOS.Dealer.Infrastructure.Mappings
{
    /// <summary>
    /// This class is used for country code value object domain model mapping
    /// </summary>
    public class CountryCodeMapping : ClassMap<CountryCode>
    {
        public CountryCodeMapping()
        {
            Table("DEALER_NETWORK");
            CompositeId()
                .KeyProperty(x => x.Code);
        }
    }
}

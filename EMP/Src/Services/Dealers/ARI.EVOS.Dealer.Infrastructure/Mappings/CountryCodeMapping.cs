using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using FluentNHibernate.Mapping;

namespace EMP.Management.Infrastructure.Mappings
{
    public class CountryCodeMapping : ClassMap<CountryCode>
    {
        public CountryCodeMapping()
        {
            CompositeId()
                .KeyProperty(x => x.Code);
        }
    }
}

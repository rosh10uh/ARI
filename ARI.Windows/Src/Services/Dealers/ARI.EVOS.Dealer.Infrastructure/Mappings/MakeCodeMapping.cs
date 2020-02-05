using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using FluentNHibernate.Mapping;

namespace ARI.EVOS.Dealer.Infrastructure.Mappings
{
    /// <summary>
    /// This class is used for make code value object domain model mapping
    /// </summary>
    public class MakeCodeMapping : ClassMap<MakeCode>
    {
        public MakeCodeMapping()
        {
            Table("DEALER_NETWORK");
            CompositeId()
                .KeyProperty(x => x.Code);            
        }
    }
}

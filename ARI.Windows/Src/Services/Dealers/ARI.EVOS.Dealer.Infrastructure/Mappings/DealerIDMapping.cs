using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using FluentNHibernate.Mapping;

namespace ARI.EVOS.Dealer.Infrastructure.Mappings
{
    /// <summary>
    /// This class is used for dealer Id value object domain model mapping
    /// </summary>
    public class DealerIDMapping : ClassMap<DealerId>
    {
        public DealerIDMapping()
        {
            Table("DEALER_NETWORK");
            CompositeId()
                .KeyProperty(x => x.Id);           
        }
    }
}

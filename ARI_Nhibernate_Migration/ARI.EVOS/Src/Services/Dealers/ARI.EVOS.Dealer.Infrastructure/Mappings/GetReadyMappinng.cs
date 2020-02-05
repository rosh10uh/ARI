using ARI.EVOS.Dealer.Domain.Models.Dealer.Entity;
using FluentNHibernate.Mapping;

namespace ARI.EVOS.Dealer.Infrastructure.Mappings
{
    /// <summary>
    /// This class is used for get ready entity domain model mapping
    /// </summary>
    public class GetReadyMappinng : ClassMap<GetReady>
    {
        public GetReadyMappinng()
        {
            Table("GET_READY");
            CompositeId()
                .KeyReference(x => x.CountryCode, "COUNTRY_CODE")
                .KeyReference(x => x.DealerId, "DEALER_ID")
                .KeyReference(x => x.MakeCode, "MAKE_CODE")
                .KeyProperty(x => x.GRVehicles, "GET_READY_CATEGORY");
            Map(x => x.ClientId, "CLIENT_ID");
            Map(x => x.Amount, "GET_READY_AMT");
            Component(a => a.EffectiveDate, b =>
            {
                b.Map(c => c.Date).Column("GET_READY_EFF_DATE");
            });
            Map(x => x.LastProgram, "LAST_PRGM");
            Component(a => a.LastUser, b =>
            {
                b.Map(c => c.UserName).Column("LAST_USER");
            });
            Map(x => x.LastChange, "LAST_CHG");
        }
    }
}

using ARI.EVOS.Dealers.Models;
using System.Collections.Generic;
using Chassis.Query.Annotations;
using Chassis.Query.Interfaces;

namespace ARI.EVOS.Dealer.Query.Queries
{
    /// <summary>
    /// This class is used to set query for get dealer detail (Dapper)
    /// </summary>
    [Sql("GetDealer")]
    public class GetDealerQuery : IQuery<IEnumerable<DealerNetworkModel>>
    {
        public string CountryCode { get; private set; }
        public string MakeCode { get; private set; }
        public string DealerId { get; private set; }

        public GetDealerQuery(string countryCode,string makeCode, string dealerId)
        {
            CountryCode = countryCode;
            MakeCode = makeCode;
            DealerId = dealerId;
        }
    }
}

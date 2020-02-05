using ARI.EVOS.Dealers.Models;
using Chassis.Query.Annotations;
using Chassis.Query.Interfaces;

namespace ARI.EVOS.Dealer.Query.Queries
{
    /// <summary>
    /// This class is used to set query for get city and state based on zip (Dapper)
    /// </summary>
    [Sql("GetCityFromZip")]
    public class GetCityFromZipQuery : IQuery<DealerNetworkModel>
    {
        public string ZipCode { get; private set; }
        public GetCityFromZipQuery(string zipCode)
        {
            ZipCode = zipCode;
        }
    }
}

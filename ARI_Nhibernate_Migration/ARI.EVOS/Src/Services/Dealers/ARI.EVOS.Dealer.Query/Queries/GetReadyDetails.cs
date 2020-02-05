using ARI.EVOS.Dealers.Models;
using Chassis.Query.Annotations;
using Chassis.Query.Interfaces;
using System.Collections.ObjectModel;

namespace ARI.EVOS.Dealer.Query.Queries
{
    /// <summary>
    /// This class is used to set query for get get dealer's details (Dapper)
    /// </summary>
    [Sql("GetReadyDetails")]
    public class GetReadyDetails : IQuery<ObservableCollection<GetReadyModel>>
    {
        public string CountryCode { get; private set; }
        public string MakeCode { get; private set; }
        public string DealerId { get; private set; }
        public GetReadyDetails(string countryCode,string makeCode,string dealerId)
        {
            CountryCode = countryCode;
            MakeCode = makeCode;
            DealerId = dealerId;
        }
    }
}

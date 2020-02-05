using ARI.EVOS.Dealers.Models;
using System.Collections.ObjectModel;
using Chassis.Query.Annotations;
using Chassis.Query.Interfaces;

namespace ARI.EVOS.Dealer.Query.Queries
{
    /// <summary>
    /// This class is used to set query for get contact email (Dapper)
    /// </summary>
    [Sql("GetContactEmailDetail")]
    public class GetContactEmailDetailQuery : IQuery<ObservableCollection<ContactEmailModel>>
    {
        public string CountryCode { get; private set; }
        public string MakeCode { get; private set; }
        public string DealerId { get; private set; }
        public GetContactEmailDetailQuery(string countryCode, string makeCode, string dealerId)
        {
            CountryCode = countryCode;
            MakeCode = makeCode;
            DealerId = dealerId;
        }
    }
}

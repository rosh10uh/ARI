using ARI.EVOS.Dealers.Models;
using System.Collections.ObjectModel;
using Chassis.Query.Annotations;
using Chassis.Query.Interfaces;

namespace ARI.EVOS.Dealer.Query.Queries
{
    /// <summary>
    /// This class is used to set query to get data of dealer search (Dapper)
    /// </summary>
    [Sql("SearchDealers")]
    public class SearchDealersQuery : IQuery<ObservableCollection<DealerSearchModel>>
    {
        public string CountryCode { get; private set; }
        public string MakeCode { get; private set; }
        public string DealerId { get; private set; }
        public string VendorName { get; private set; }
        public SearchDealersQuery(string countryCode, string makeCode, string dealerId, string vendorName)
        {
            CountryCode = countryCode;
            MakeCode = makeCode;
            DealerId = dealerId;
            VendorName = vendorName;
        }
    }
}

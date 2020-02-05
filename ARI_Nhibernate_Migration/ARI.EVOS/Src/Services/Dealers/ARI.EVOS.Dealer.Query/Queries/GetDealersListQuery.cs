using ARI.EVOS.Dealers.Models;
using System.Collections.ObjectModel;
using Chassis.Query.Annotations;
using Chassis.Query.Interfaces;

namespace ARI.EVOS.Dealer.Query.Queries
{
    /// <summary>
    /// This class is used to set query for get dealers list (Dapper)
    /// </summary>
    [Sql("GetDealersList")]
    public class GetDealersListQuery : IQuery<ObservableCollection<DealerSearchModel>>
    {
        public GetDealersListQuery()
        {

        }
    }
}


using ARI.EVOS.Common.Models;
using System.Collections.ObjectModel;
using Chassis.Query.Annotations;
using Chassis.Query.Interfaces;

namespace ARI.EVOS.Dealer.Query.Queries
{
    /// <summary>
    /// This class is used to set query for get country and make details (Dapper)
    /// </summary>
    [Sql("GetCountryDetail")]
    public class GetCountryDetailQuery : IQuery<ObservableCollection<CountryModel>>
    {
        public GetCountryDetailQuery()
        { }          
     
    }
}

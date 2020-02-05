
using ARI.EVOS.Common.Models;
using System.Collections.ObjectModel;
using Chassis.Query.Annotations;
using Chassis.Query.Interfaces;

namespace ARI.EVOS.Dealer.Query.Queries
{
    /// <summary>
    /// This class is used to set query for get make's details (Dapper)
    /// </summary>
    [Sql("GetMakeDetail")]
    public class GetMakeDetailQuery : IQuery<ObservableCollection<MakeModel>>
    {
        public GetMakeDetailQuery()
        { }        
    }
}

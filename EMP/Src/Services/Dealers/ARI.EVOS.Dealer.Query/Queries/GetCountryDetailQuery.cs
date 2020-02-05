using ARI.EVOS.Common.Models;
using Chassis.Query.Annotations;
using Chassis.Query.Interfaces;
using System.Collections.ObjectModel;

namespace EMP.Management.Query.Queries
{
    [Sql("GetCountryDetail")]
    public class GetCountryDetailQuery : IQuery<ObservableCollection<CountryModel>>
    {
        public GetCountryDetailQuery()
        { }

    }
}

using Chassis.Query.Annotations;
using Chassis.Query.Interfaces;
using EMP.Management.Models;
using System.Collections.ObjectModel;

namespace EMP.Management.Query.Queries
{
    [Sql("GetEmployee")]
    public class GetEmployeeQuery : IQuery<ObservableCollection<EmployeeModel>>
    {
        public string EmployeeId { get; private set; }
        public string CountryCode { get; private set; }
        public GetEmployeeQuery(string employeeId, string countryCode)
        {
            EmployeeId = employeeId;
            CountryCode = countryCode;
        }
    }
}

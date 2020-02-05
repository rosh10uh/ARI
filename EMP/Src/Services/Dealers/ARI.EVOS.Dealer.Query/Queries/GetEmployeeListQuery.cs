using Chassis.Query.Annotations;
using Chassis.Query.Interfaces;
using EMP.Management.Models;
using System.Collections.ObjectModel;

namespace EMP.Management.Query.Queries
{
    [Sql("GetEmployeesList")]
    public class GetEmployeeListQuery : IQuery<ObservableCollection<EmployeeModel>>
    {
        public GetEmployeeListQuery()
        {

        }
    }
}

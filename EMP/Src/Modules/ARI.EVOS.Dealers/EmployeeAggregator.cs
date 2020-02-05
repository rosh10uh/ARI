using EMP.Management.Models;
using Prism.Events;

namespace EMP.Management
{
    public class EmployeeAggregator : PubSubEvent<EmployeeModel>
    {
    }
}

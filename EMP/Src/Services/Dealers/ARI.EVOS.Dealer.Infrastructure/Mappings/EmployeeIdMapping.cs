using EMP.Management.Domain.Models.Employee.ValueObject;
using FluentNHibernate.Mapping;

namespace EMP.Management.Infrastructure.Mappings
{
    public class EmployeeIdMapping : ClassMap<EmployeeId>
    {
        public EmployeeIdMapping()
        {
            CompositeId()
                .KeyProperty(x => x.Id);
        }
    }
}

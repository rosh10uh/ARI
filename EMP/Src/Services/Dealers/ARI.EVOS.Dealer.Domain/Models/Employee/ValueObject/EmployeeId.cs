using Chassis.Domain.Aggregate;

namespace EMP.Management.Domain.Models.Employee.ValueObject
{
    public class EmployeeId : ValueObject<EmployeeId>
    {
        public virtual string Id { get; protected set; }
        protected EmployeeId()
        {
        }

        private EmployeeId(string id)
        {
            Id = id;
        }

        public static EmployeeId Create(string id)
        {
            return new EmployeeId(id);
        }

        protected override bool EqualsCore(EmployeeId employeeId)
        {
            return Id == employeeId.Id;
        }

        protected override int GetHashCodeCore()
        {
            return 13 * Id.Length;
        }
    }
}

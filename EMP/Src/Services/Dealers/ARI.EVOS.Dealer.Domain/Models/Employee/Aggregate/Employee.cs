using ARI.EVOS.Dealer.Domain.Models.Dealer.Entity;
using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using Chassis.Domain.Aggregate;
using EMP.Management.Domain.Models.Employee.ValueObject;

namespace EMP.Management.Domain.Models.Employee.Aggregate
{
    public class Employee : AggregateRoot<int>
    {
        public virtual EmployeeId EmployeeId { get; protected set; }
        public virtual string FirstName { get; protected set; }
        public virtual string LastName { get; protected set; }
        public virtual CountryCode CountryCode { get; protected set; }
        public virtual Email Email { get; protected set; }
        public virtual BankDetail BankDetail { get; protected set; }
        public virtual Address Address { get; protected set; }

        protected Employee()
        {

        }

        private Employee(EmployeeId employeeId, CountryCode countryCode)
        {
            EmployeeId = employeeId;
            CountryCode = countryCode;
        }

        public static Employee Create(EmployeeId employeeId, CountryCode countryCode)
        {
            return new Employee(employeeId, countryCode);
        }

        public virtual void SetEmployeeInfo(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public virtual void SetAddress(Address address)
        {
            Address = address;
        }
        public virtual void SetBankDetails(BankDetail bankDetail)
        {
            BankDetail = bankDetail;
        }

        public virtual void SetEmail(Email email)
        {
            Email = email;
        }

    }
}

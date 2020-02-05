using Chassis.Domain.Aggregate;

namespace EMP.Management.Domain.Models.Employee.ValueObject
{
    public class Address : ValueObject<Address>
    {
        public virtual string Address1 { get; protected set; }
        public virtual string Address2 { get; protected set; }
        public virtual string Address3 { get; protected set; }
        public virtual string LandMark { get; protected set; }

        protected Address()
        {
        }

        private Address(string address1, string address2, string address3, string landMark)
        {
            Address1 = address1;
            Address2 = address2;
            Address3 = address3;
            LandMark = landMark;
        }

        public static Address Create(string address1, string address2, string address3, string landMark)
        {
            return new Address(address1, address2, address3, landMark);
        }

        protected override bool EqualsCore(Address address)
        {
            return Address1 == address.Address1 && Address2 == address.Address2 && Address3 == address.Address3 && LandMark == address.LandMark;
        }

        protected override int GetHashCodeCore()
        {
            return base.GetHashCode();
        }

    }
}

using Chassis.Domain.Aggregate;

namespace ARI.EVOS.Dealer.Domain.Models.Dealer.Entity
{
    /// <summary>
    /// This class is used to store dealer address details
    /// </summary>
    public class Address : Entity<int>
    {
        public virtual string Address1 { get; protected set; }
        public virtual string Address2 { get; protected set; }
        public virtual string Address3 { get; protected set; }
        public virtual string Address4 { get; protected set; }
        public virtual string State { get; protected set; }
        public virtual string City { get; protected set; }
        public virtual string ZipCode { get; protected set; }
        public virtual string ZipPlus4 { get; protected set; }
        protected Address()
        {
        }

        private Address(string city, string zipCode, string state, string address1, string address2, string address3, string address4)
        {
            Address1 = address1;
            Address2 = address2;
            Address3 = address3;
            Address4 = address4;
            State = state;
            City = city;
            State = state;
            ZipCode = zipCode;
        }

        public static Address Create(string city, string zipCode, string state, string address1, string address2, string address3, string address4)
        {
            return new Address(city, zipCode, state, address1, address2, address3, address4);
        }

        public void Update(string city, string zipCode, string state, string address1, string address2, string address3, string address4)
        {
            Address1 = address1;
            Address2 = address2;
            Address3 = address3;
            Address4 = address4;
            State = state;
            City = city;
            ZipCode = zipCode;
        }
    }
}

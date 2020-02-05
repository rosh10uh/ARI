using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using Chassis.Domain.Aggregate;

namespace ARI.EVOS.Dealer.Domain.Models.Dealer.Entity
{
    /// <summary>
    /// This class is used to store dealer contact Details
    /// </summary>
    public class ContactInformation : Entity<int>
    {
        public virtual string Contact1 { get; protected set; }
        public virtual string Contact2 { get; protected set; }
        public virtual PhoneNumber PhoneNumber1 { get; protected set; }
        public virtual PhoneNumber PhoneNumber2 { get; protected set; }
        public virtual string PhoneExchange1 { get; protected set; }
        public virtual string PhoneExchange2 { get; protected set; }
        public virtual Fax Fax { get; protected set; }
        public virtual Email Email { get; protected set; }

        protected ContactInformation()
        {
        }

        private ContactInformation(string contact1, string contact2, PhoneNumber phoneNumber1, PhoneNumber phoneNumber2, string phoneExchange1, string phoneExchange2)
        {
            Contact1 = contact1;
            Contact2 = contact2;
            PhoneNumber1 = phoneNumber1;
            PhoneNumber2 = phoneNumber2;
            PhoneExchange1 = phoneExchange1;
            PhoneExchange2 = phoneExchange2;

        }

        public static ContactInformation Create(string contact1, string contact2, PhoneNumber phoneNumber1, PhoneNumber phoneNumber2, string phoneExchange1, string phoneExchange2)
        {
            return new ContactInformation(contact1, contact2, phoneNumber1, phoneNumber2, phoneExchange1, phoneExchange2);
        }

        public void SetOtherContactInformation(Fax fax, Email email)
        {
            Fax = fax;
            Email = email;
        }

        public void Update(string contact1, string contact2, PhoneNumber phoneNumber1, PhoneNumber phoneNumber2, string phoneExchange1, string phoneExchange2)
        {
            Contact1 = contact1;
            Contact2 = contact2;
            PhoneNumber1 = phoneNumber1;
            PhoneNumber2 = phoneNumber2;
            PhoneExchange1 = phoneExchange1;
            PhoneExchange2 = phoneExchange2;
        }
    }
}

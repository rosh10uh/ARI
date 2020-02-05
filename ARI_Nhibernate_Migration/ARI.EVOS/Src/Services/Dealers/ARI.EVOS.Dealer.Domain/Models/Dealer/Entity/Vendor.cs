using Chassis.Domain.Aggregate;

namespace ARI.EVOS.Dealer.Domain.Models.Dealer.Entity
{
    /// <summary>
    /// This class is used to store dealer vendor details
    /// </summary>
    public class Vendor : Entity<int>
    {
        public virtual string VendorId { get; protected set; }
        public virtual string Name { get; protected set; }
        protected Vendor()
        {
        }

        private Vendor(string vendorId, string name)
        {
            VendorId = vendorId;
            Name = name;
        }

        public static Vendor Create(string vendorId, string name)
        {
            return new Vendor(vendorId, name);
        }

        public void Update(string vendorId, string name)
        {
            VendorId = vendorId;
            Name = name;
        }
    }
}

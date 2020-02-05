using System;
using System.Linq;
using Chassis.Domain.Aggregate;

namespace ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject
{
    /// <summary>
    /// This class is used to store selling delivery value
    /// </summary>
    public class SellingDelivery : ValueObject<SellingDelivery>
    {
        public virtual string SellingDeliveryId { get; protected set; }

        protected SellingDelivery()
        {
        }

        private SellingDelivery(string sellingDeliveryId)
        {
            SellingDeliveryId = sellingDeliveryId;
        }

        public static SellingDelivery Create(string sellingDeliveryId)
        {
            var sellingDelivery = new SellingDelivery(sellingDeliveryId);
            return sellingDelivery.IsValid() ? sellingDelivery : throw new Exception(DealerDomainConstant.ErrSellingDeliveryInitials);
        }
        
        private bool IsValid()
        {
            if (!string.IsNullOrEmpty(SellingDeliveryId))
            {
                string[] sellingDeliveryArray = { "S", "D", "B" };
                return sellingDeliveryArray.Contains(SellingDeliveryId.ToUpper());
            }
            return true;
        }
        protected override bool EqualsCore(SellingDelivery sellingDelivery)
        {
            return SellingDeliveryId == sellingDelivery.SellingDeliveryId;
        }

        protected override int GetHashCodeCore()
        {
            return base.GetHashCode();
        }
    }
}

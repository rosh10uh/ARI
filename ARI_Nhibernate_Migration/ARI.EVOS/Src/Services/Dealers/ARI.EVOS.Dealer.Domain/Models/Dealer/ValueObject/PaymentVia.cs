using System;
using System.Linq;
using Chassis.Domain.Aggregate;

namespace ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject
{
    /// <summary>
    /// This class is used to store payment via value
    /// </summary>
    public class PaymentVia : ValueObject<PaymentVia>
    {
        public virtual string PaymentType { get; protected set; }

        protected PaymentVia()
        {
        }
        private PaymentVia(string paymentType)
        {
            PaymentType = paymentType;
        }

        public static PaymentVia Create(string paymentType)
        {
            var paymentVia = new PaymentVia(paymentType);
            return paymentVia.IsValid() ? paymentVia : throw new Exception(DealerDomainConstant.ErrPaymentViaInitials);
        }
        
        public bool IsValid()
        {
            if (!string.IsNullOrEmpty(PaymentType))
            {
                string[] paymentTypeArray = { "C", "D", "E", "W", "A" };

                return !string.IsNullOrEmpty(PaymentType) && paymentTypeArray.Contains(PaymentType.ToUpper());
            }
            return true;
        }
        protected override bool EqualsCore(PaymentVia paymentVia)
        {
            return PaymentType == paymentVia.PaymentType;
        }

        protected override int GetHashCodeCore()
        {
            return base.GetHashCode();
        }
    }
}

using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using Chassis.Domain.Aggregate;

namespace ARI.EVOS.Dealer.Domain.Models.Dealer.Entity
{
    /// <summary>
    /// This class is used to store dealer additional details
    /// </summary>
    public class DealerOtherInformation : Entity<int>
    {
        public virtual BacCode BacCode { get; protected set; }
        public virtual SellingDelivery SellingDelivery { get; protected set; }
        public virtual MinoryIndicator MinoryIndicator { get; protected set; }
        public virtual DealerRating DealerRating { get; protected set; }
        public virtual string ManufactureZoneCode { get; protected set; }
        public virtual string PrintDealerDraft { get; protected set; }
        public virtual string DraftAccount1 { get; protected set; }
        public virtual string DraftAccount2 { get; protected set; }
        public virtual string TaxID { get; protected set; }
        public virtual string OverrideTerms { get; protected set; }
        public virtual string PaytoVendor { get; protected set; }
        public virtual int? FactoryCount { get; protected set; }
        public virtual int? StockCount { get; protected set; }
        public virtual PaymentVia PaymentVia { get; protected set; }
        public virtual string CourtesyDelivery { get; protected set; }
        public virtual string Certification { get; protected set; }

        protected DealerOtherInformation()
        {
        }

        private DealerOtherInformation(DealerRating rating, PaymentVia paymentVia)
        {
            DealerRating = rating;
            PaymentVia = paymentVia;
        }

        public static DealerOtherInformation Create(DealerRating rating, PaymentVia paymentVia)
        {
            return new DealerOtherInformation(rating, paymentVia);
        }
        public void Update(DealerRating rating, PaymentVia paymentVia, BacCode bAcCode, SellingDelivery sellingDelivery, MinoryIndicator minoryIndicator)
        {
            DealerRating = rating;
            PaymentVia = paymentVia;
            BacCode = bAcCode;
            SellingDelivery = sellingDelivery;
            MinoryIndicator = minoryIndicator;
        }

        public void UpdateOtherInformation(string manufactureZoneCode, string printDealerDraft,
            string draftAccount1, string draftAccount2, string taxId, string overrideTerms,
            string paytoVendor, int? factoryCount, int? stockCount, string courtesyDelivery, string certification)
        {
            ManufactureZoneCode = manufactureZoneCode;
            PrintDealerDraft = printDealerDraft;
            DraftAccount1 = draftAccount1;
            DraftAccount2 = draftAccount2;
            TaxID = taxId;
            OverrideTerms = overrideTerms;
            PaytoVendor = paytoVendor;
            FactoryCount = factoryCount;
            StockCount = stockCount;
            CourtesyDelivery = courtesyDelivery;
            Certification = certification;
        }

        public void SetInformation(BacCode bAcCode, SellingDelivery sellingDelivery, MinoryIndicator minoryIndicator)
        {
            BacCode = bAcCode;
            SellingDelivery = sellingDelivery;
            MinoryIndicator = minoryIndicator;
        }

        public void SetOtherInformation(string manufactureZoneCode, string printDealerDraft,
            string draftAccount1, string draftAccount2, string taxId, string overrideTerms,
            string paytoVendor, int? factoryCount, int? stockCount, string courtesyDelivery, string certification)
        {
            ManufactureZoneCode = manufactureZoneCode;
            PrintDealerDraft = printDealerDraft;
            DraftAccount1 = draftAccount1;
            DraftAccount2 = draftAccount2;
            TaxID = taxId;
            OverrideTerms = overrideTerms;
            PaytoVendor = paytoVendor;
            FactoryCount = factoryCount;
            StockCount = stockCount;
            CourtesyDelivery = courtesyDelivery;
            Certification = certification;
        }
    }
}

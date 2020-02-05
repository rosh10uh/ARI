using FluentNHibernate.Mapping;

namespace ARI.EVOS.Dealer.Infrastructure.Mappings
{
    /// <summary>
    /// This class is used for dealer entity domain model mapping
    /// </summary>
    public class DealerMapping : ClassMap<Domain.Models.Dealer.Aggregate.Dealer>
    {
        public DealerMapping()
        {
            //// Dealer 
            Table("DEALER_NETWORK");
            CompositeId()
                .KeyReference(x => x.CountryCode, "COUNTRY_CODE")
                .KeyReference(x => x.DealerId, "DEALER_ID")
                .KeyReference(x => x.MakeCode, "MAKE_CODE");
            Component(a => a.Vendor, b =>
            {
                b.Map(c => c.VendorId).Column("VENDOR_ID");
                b.Map(c => c.Name).Column("VENDOR_NAME");
            });
            //Set Contact Domain
            Component(a => a.ContactInformation, b =>
            {
                b.Map(c => c.Contact1).Column("CONTACT1");
                b.Map(c => c.Contact2).Column("CONTACT2");
                b.Map(c => c.PhoneExchange1).Column("PHONE1_EXCHANGE");
                b.Map(c => c.PhoneExchange2).Column("PHONE2_EXCHANGE");
                b.Component(p => p.Fax, q =>
                {
                    q.Map(r => r.FaxNumber).Column("FAX_NUMBER");
                });

                b.Component(p => p.Email, q =>
                {
                    q.Map(r => r.EmailAddress).Column("EMAIL");
                });

                b.Component(p => p.PhoneNumber1, q =>
                {
                    q.Map(r => r.Phone).Column("PHONE1_NUMBER");
                });

                b.Component(p => p.PhoneNumber2, q =>
                {
                    q.Map(r => r.Phone).Column("PHONE2_NUMBER");
                });
            });
            //Set Address Domain
            Component(a => a.Address, b =>
            {
                b.Map(c => c.Address1).Column("ADDRESS1");
                b.Map(c => c.Address2).Column("ADDRESS2");
                b.Map(c => c.Address3).Column("ADDRESS3");
                b.Map(c => c.Address4).Column("ADDRESS4");
                b.Map(c => c.State).Column("STATE");
                b.Map(c => c.City).Column("CITY");
                b.Map(c => c.ZipCode).Column("ZIP_CODE");
                b.Map(c => c.ZipPlus4).Column("ZIP_PLUS4");
            });

            Map(x => x.Comment, "DEALER_COMMENTS");
            Map(x => x.CreationProgram, "CREATION_PRGM");
            Component(a => a.CreationUser, b =>
            {
                b.Map(c => c.UserName).Column("CREATION_USER");
            });

            Map(x => x.CreationDate, "CREATION_DATE");
            Map(x => x.LastUsedDate, "LAST_USED_DATE");
            Map(x => x.LastProgram, "LAST_PRGM");
            Component(a => a.LastUser, b =>
            {
                b.Map(c => c.UserName).Column("LAST_USER");
            });
            Map(x => x.LastChg, "LAST_CHG");

            // Set Additional Information Domain
            Component(a => a.AdditionalInformation, b =>
            {
                b.Component(p => p.BacCode, q =>
                {
                    q.Map(r => r.Bac).Column("GM_BUSN_ASCT_CD");
                });

                b.Component(p => p.SellingDelivery, q =>
                {
                    q.Map(r => r.SellingDeliveryId).Column("SELLING_DELIV_IND");
                });

                b.Component(p => p.MinoryIndicator, q =>
                {
                    q.Map(r => r.MinIndicator).Column("MINORITY_IND");
                });
                b.Component(p => p.DealerRating, q =>
                {
                    q.Map(r => r.Rating).Column("DEALER_RATING");
                });

                b.Map(c => c.ManufactureZoneCode).Column("MFG_ZONE_CODE");
                b.Map(c => c.PrintDealerDraft).Column("PRINT_DEALER_DRAFT");
                b.Map(c => c.DraftAccount1).Column("DRAFT_ACCT1");
                b.Map(c => c.DraftAccount2).Column("DRAFT_ACCT2");
                b.Map(c => c.TaxID).Column("TAX_ID");
                b.Map(c => c.OverrideTerms).Column("TERMS_OVERRIDE");
                b.Map(c => c.PaytoVendor).Column("PAY_TO_VENDOR");
                b.Map(c => c.FactoryCount).Column("FACTORY_COUNT_PRIOR12");
                b.Map(c => c.StockCount).Column("STOCK_COUNT_PRIOR12");
                b.Component(p => p.PaymentVia, q =>
                {
                    q.Map(r => r.PaymentType).Column("PYMT_VIA");
                });
                b.Map(c => c.CourtesyDelivery).Column("COURTESY_DELIV_PRINT_IND");
                b.Map(c => c.Certification).Column("CERT_IND");

                Map(x => x.KeyDealerClient, "KD_CLIENT");
                Map(x => x.KeyDealerDiv, "KD_DIV");
                Map(x => x.RebateVolumeFactory, "CAN_VOLUME_FACTORY");
                Map(x => x.RebateVolumeStock, "CAN_VOLUME_STOCK");
                Map(x => x.UKCommission, "UK_COMMISSION");
            });

            // Set Bank Domain
            Component(a => a.BankDetail, b =>
            {
                b.Map(c => c.BankNumber).Column("WIRE_BANK_NUMB");
                b.Component(p => p.BankAccount, q =>
                {
                    q.Map(r => r.AccountNumber).Column("WIRE_BANK_ACCT");
                });
                b.Map(c => c.BankName).Column("WIRE_BANK_NAME");
                b.Component(p => p.BankCity, q =>
                {
                    q.Map(r => r.City).Column("WIRE_BANK_CITY");
                });

            });
        }
    }
}

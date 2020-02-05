using Chassis.Command.Interfaces;
using System;

namespace ARI.EVOS.Dealer.Command.Commands
{
    /// <summary>
    /// This class contains add dealer command properties
    /// </summary>
    public class DealerCommand : ICommand<string>
    {
        public string CountryCode { get; private set; }
        public string DealerId { get; private set; }
        public string MakeCode { get; private set; }
        public string VendorId { get; private set; }
        public string VendorName { get; private set; }
        public string Address1 { get; private set; }
        public string Address2 { get; private set; }
        public string Address3 { get; private set; }
        public string Address4 { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string ZipCode { get; private set; }
        public string ZipPlus4 { get; private set; }
        public string Contact1 { get; private set; }
        public string Contact2 { get; private set; }
        public string Phone1AreaCode { get; private set; }
        public string Phone1Exchange { get; private set; }
        public string Phone1Number { get; private set; }
        public string Phone1Extension { get; private set; }
        public string Phone2AreaCode { get; private set; }
        public string Phone2Exchange { get; private set; }
        public string Phone2Number { get; private set; }
        public string Phone2Extension { get; private set; }
        public string FaxAreaCode { get; private set; }
        public string FaxExchange { get; private set; }
        public string FaxNumber { get; private set; }
        public string GmBusnAsctCd { get; private set; }
        public string SellingDelivInd { get; private set; }
        public string MinorityInd { get; private set; }
        public string DealerRating { get; private set; }
        public string DraftAcct1 { get; private set; }
        public string DraftAcct2 { get; private set; }
        public string TaxId { get; private set; }
        public string PrintDealerDraft { get; private set; }
        public string MfgZoneCode { get; private set; }
        public int? FactoryCountPrior12 { get; private set; }
        public int? StockCountPrior12 { get; private set; }
        public string DealerComments { get; private set; }
        public string CreationPrgm { get; private set; }
        public string CreationUser { get; private set; }
        public DateTime? CreationDate { get; private set; }
        public DateTime? LastUsedDate { get; private set; }
        public string LastPrgm { get; private set; }
        public string LastUser { get; private set; }
        public DateTime? LastChg { get; private set; }
        public string Email { get; private set; }
        public string VendorIdSetupInd { get; private set; }
        public string BankNumber { get; private set; }
        public string BankName { get; private set; }
        public string BankCity { get; private set; }
        public string BankAccount { get; private set; }
        public string PymtVia { get; private set; }
        public string OrigRecSource { get; private set; }
        public string GmBusnFuncCd { get; private set; }
        public string GmSeScNo { get; private set; }
        public string GmSiteTypNo { get; private set; }
        public string LastRecSource { get; private set; }
        public string PayToVendor { get; private set; }
        public string DealerLicenseRestrictions { get; private set; }
        public string DealerLicenseComment { get; private set; }
        public string TypeOfDealer { get; private set; }
        public string TermsOverride { get; private set; }
        public string CourtesyDelivPrintInd { get; private set; }
        public string CertInd { get; private set; }
        public string StatusReportPrintInd { get; private set; }
        public string StatusReportContact { get; private set; }
        public string StatusReportPhoneAreaCode { get; private set; }
        public string StatusReportPhoneExchange { get; private set; }
        public string StatusReportPhoneNumber { get; private set; }
        public string StatusReportFaxAreaCode { get; private set; }
        public string StatusReportFaxExchange { get; private set; }
        public string StatusReportFaxNumber { get; private set; }
        public string StatusReportEmail1 { get; private set; }
        public string StatusReportEmail2 { get; private set; }
        public string StatusReportEmail3 { get; private set; }
        public string StatusReportEmail4 { get; private set; }
        public int? CanVolumeFactory { get; private set; }
        public int? CanVolumeStock { get; private set; }
        public string UvTurnInInd { get; private set; }
        public string KdClient { get; private set; }
        public string KdDiv { get; private set; }
        public string Phone1NumberIntl { get; private set; }
        public string Phone1NumberExtensionIntl { get; private set; }
        public string Phone2NumberIntl { get; private set; }
        public string Phone2NumberExtensionIntl { get; private set; }
        public string StatusReportPhoneNumIntl { get; private set; }
        public string StatusReportFaxNumberIntl { get; private set; }
        public string FaxNumberIntl { get; private set; }
        public float? UkCommission { get; private set; }
        public DealerCommand()
        {

        }
    }
}

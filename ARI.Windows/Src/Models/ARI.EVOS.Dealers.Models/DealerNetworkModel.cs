using System;
using System.ComponentModel.DataAnnotations;

namespace ARI.EVOS.Dealers.Models
{
    /// <summary>
    /// This class is used to initialize properties of Dealer Network model 
    /// </summary>
    public class DealerNetworkModel 
    {       
        public string CountryCode { get; set; }     
        public string DealerId { get; set; }        
        public string MakeCode { get; set; }
        public string VendorId { get; set; }
        public string VendorName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string ZipPlus4 { get; set; }
        public string Contact1 { get; set; }
        public string Contact2 { get; set; }
        public string Phone1AreaCode { get; set; }
        public string Phone1Exchange { get; set; }
        public string Phone1Number { get; set; }
        public string Phone1Extension { get; set; }
        public string Phone2AreaCode { get; set; }
        public string Phone2Exchange { get; set; }
        public string Phone2Number { get; set; }
        public string Phone2Extension { get; set; }
        public string FaxAreaCode { get; set; }
        public string FaxExchange { get; set; }
        public string FaxNumber { get; set; }
        public string GmBusnAsctCd { get; set; }
        public string SellingDelivInd { get; set; }
        public string MinorityInd { get; set; }
        public string DealerRating { get; set; }
        public string DraftAcct1 { get; set; }
        public string DraftAcct2 { get; set; }
        public string TaxId { get; set; }
        public string PrintDealerDraft { get; set; }
        public string MfgZoneCode { get; set; }
        public int? FactoryCountPrior12 { get; set; }
        public int? StockCountPrior12 { get; set; }
        public string DealerComments { get; set; }
        public string CreationPrgm { get; set; }
        public string CreationUser { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastUsedDate { get; set; }
        public string LastPrgm { get; set; }
        public string LastUser { get; set; }
        public DateTime? LastChg { get; set; }
        public string Email { get; set; }
        public string VendorIdSetupInd { get; set; }
        public string BankNumber { get; set; }
        public string BankName { get; set; }
        public string BankCity { get; set; }        
        public string BankAccount { get; set; }
        public string PymtVia { get; set; }
        public string OrigRecSource { get; set; }
        public string GmBusnFuncCd { get; set; }
        public string GmSeScNo { get; set; }
        public string GmSiteTypNo { get; set; }
        public string LastRecSource { get; set; }
        public string PayToVendor { get; set; }
        public string DealerLicenseRestrictions { get; set; }
        public string DealerLicenseComment { get; set; }
        public string TypeOfDealer { get; set; }
        public string TermsOverride { get; set; }
        public string CourtesyDelivPrintInd { get; set; }
        public string CertInd { get; set; }
        public string StatusReportPrintInd { get; set; }
        public string StatusReportContact { get; set; }
        public string StatusReportPhoneAreaCode { get; set; }
        public string StatusReportPhoneExchange { get; set; }
        public string StatusReportPhoneNumber { get; set; }
        public string StatusReportFaxAreaCode { get; set; }
        public string StatusReportFaxExchange { get; set; }
        public string StatusReportFaxNumber { get; set; }
        public string StatusReportEmail1 { get; set; }
        public string StatusReportEmail2 { get; set; }
        public string StatusReportEmail3 { get; set; }
        public string StatusReportEmail4 { get; set; }
        public int? CanVolumeFactory { get; set; }
        public int? CanVolumeStock { get; set; }
        public string UvTurnInInd { get; set; }
        public string KdClient { get; set; }
        public string KdDiv { get; set; }
        public string Phone1NumberIntl { get; set; }
        public string Phone1NumberExtensionIntl { get; set; }
        public string Phone2NumberIntl { get; set; }
        public string Phone2NumberExtensionIntl { get; set; }
        public string StatusReportPhoneNumIntl { get; set; }
        public string StatusReportFaxNumberIntl { get; set; }
        public string FaxNumberIntl { get; set; }
        public float? UkCommission { get; set; } = 0;     
        public ContactEmailModel ContactEmail { get; set; }
    }
}

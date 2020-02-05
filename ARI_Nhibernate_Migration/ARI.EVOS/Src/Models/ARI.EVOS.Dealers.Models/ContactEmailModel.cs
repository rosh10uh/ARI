using Prism.Validation;

namespace ARI.EVOS.Dealers.Models
{
    /// <summary>
    /// This class is used to initialize properties of Contact Email model 
    /// </summary>
    public class ContactEmailModel: ValidatableBindableBase
    {
        public string DealerId { get; set; }
        public string MakeCode { get; set; }
        public string CountryCode { get; set; }
        public string ContactType { get; set; }
        public string StockContactName { get; set; }
        public string StockContactEmail { get; set; }
        public string FinanceContactName { get; set; }
        public string FinanceContactEmail { get; set; }
        public string LicenseContactName { get; set; }
        public string LicenseContactEmail { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }

    }
}

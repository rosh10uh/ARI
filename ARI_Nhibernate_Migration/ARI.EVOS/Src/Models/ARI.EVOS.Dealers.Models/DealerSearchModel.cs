namespace ARI.EVOS.Dealers.Models
{
    /// <summary>
    /// This class is used to initialize properties of Dealer Search model
    /// </summary>
    public class DealerSearchModel
    {
        public string CountryCode { get; set; }
        public string DealerId { get; set; }
        public string MakeCode { get; set; }
        public string Make { get; set; }
        public string VendorId { get; set; }
        public string VendorName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string SellingDelivInd { get; set; }
        public string DealerRating { get; set; }
        public string DealerComments { get; set; }
    }
}

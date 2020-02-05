namespace ARI.EVOS.Dealer.Command
{
    /// <summary>
    /// Common validation messages
    /// </summary>
    public static class CommandConstant
    {
        // Validation Messages
        public const string ErrMessageCountryMakeDealerId = "Please enter countrycode, make and dealerid";
        public const string ErrMessageZipCode = "Zip Code Is Invalid.";
        public const string ErrMessagePaymentVia = "Bank Information needs to be entered when payment via = ACH";
        public const string ErrMessageCountryCode = "Country required.";
        public const string ErrMessageMakeId = "Make Description required.";
        public const string ErrMessageDealerId = "Dealer ID required.";
        public const string ErrMessageVendorId = "PayTo Vendor not valid without Vendor ID.";
        public const string ErrMessageMakeCodeByDealerId = "Blank make must be used with dealer ID's beginning w/ 'CN'";
        public const string ErrMessageEmail = "Please enter name and email";


        public const string ErrMessageEmailOnCourtesyDelivery = "Must have a valid e-mail address for Courtesy Delivery Option to be EMAIL. Please enter an E-Mail address";
        public const string ErrMessageFaxOnCourtesyDelivery = "Must have a valid fax number for Courtesy Delivery Option to be FAX. Please enter a FAX Number";
        public const string ErrMessageContactOnCourtesyDelivery = "Must have a valid Contact Name for Courtesy Delivery Option to be FAX. Please enter a Contact Name";
        public const string ErrMessageRatingRange1 = "Dealer Rating must be between 1 and 9, please correct.";
        public const string ErrMessageRatingRange2 = "Dealer Rating must be between 2 and 9, please correct.";
        public const string ErrMessageDealerNotExist = "Dealer must be added before GetReady can be saved.";
        public const string ErrMessageRequiredGRVehicle = "Get Ready Category required.";
        public const string ErrMessageGRVehicleExist = "category already on file, \n Use update function to change.";
        public const string ErrMessageGRVehicleNotExist = "category not found, \n Use add function.";
        public const string ErrMessageGRVehicleSelected = "Select Get Ready Category";
        public const string ErrMessageGetReadyDeleted = " category not found,\n select from grid to delete.";
        public const string DealerExistValidation = "Dealer already on file, Use Update to change, or change Make/Dealer ID to Copy";
        public const string InsertedEmail = "Email added successfully";
        public const string UpdatedEmail = "Email updated successfully";
        public const string ErrMessageClientId = "Client # does not exist, please correct.";
    }
}

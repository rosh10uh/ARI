namespace ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject
{
    /// <summary>
    /// This class is used to store dealer constant values
    /// </summary>
    public static class DealerDomainConstant
    {
        public const string CountryCodeLengthErrorMessage = "Country code should be greater than or equal to 2.";

        public const string MakeCodeBasedOnCountryCode = "Blank make only valid for Canadian dealers";
        public const string AccountErrorMessage = "Please enter max 20 characters in Bank Account";
        public const string BacEmptyErrorMessage = "GM BAC code must be entered for GM dealers.";
        public const string BacNotEmptyErrorMessage = "GM BAC code only valid for GM dealers.";
        //Email validation
        public const string EmailValidateErrorMessage = "Please enter valid email address.";
        //Fax validation
        public const string FaxValidateNotUk = "Invalid FAX number. Please correct.";
        public const string FaxValidateUk = "Fax number should be 25 for UK.";
        //Phone validations
        public const string PhoneUkValidation = "Please enter valid phone number for UK.";
        public const string PhoneNotUkValidation = "Please enter valid phone number for the selected country.";
        public const string PhoneNotUkWithFormat = "Please set Phone in defined format";
        public const string PhoneInvalid = "Invalid Phone number.";
        public const string ErrMinorityIndicator = "Please enter only Y, N or leave blank";
        public const string ErrPaymentViaInitials = "Payment Via must be 'C' 'D' 'E' 'W' 'A' or blank.";
        public const string ErrSellingDeliveryInitials = "Selling/Delivery indicator must be 'S' 'D' or 'B'.";
        public const string EffectDateValidateErrorMessage = "Effective date must be a valid date.";


        public const string ZipCode = "Zip Code is invalid.";
    }
}

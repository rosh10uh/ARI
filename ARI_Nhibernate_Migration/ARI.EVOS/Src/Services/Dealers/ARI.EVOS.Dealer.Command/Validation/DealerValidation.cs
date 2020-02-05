using ARI.EVOS.Dealer.Command.Commands;
using ARI.EVOS.Dealer.Command.Interface;
using System;

namespace ARI.EVOS.Dealer.Command.Validation
{
    /// <summary>
    /// This class is responsible for dealer business validation
    /// </summary>
    public class DealerValidation : IDealerValidation
    {
        public DealerValidation()
        {
        }

        /// <summary>
        /// It is responsible to validate business
        /// </summary>
        /// <param name="command">AddDealerNetworkCommand</param>
        /// <returns>bool</returns>
        public bool IsValid(DealerCommand command)
        {
            return IsValidDealer(command.DealerId) && IsValidCountry(command.CountryCode) &&
                   IsValidMake(command.MakeCode) && IsValidMakeByDealerId(command.DealerId, command.MakeCode) &&
                   IsValidRating(command.DealerRating) && IsValidVendorId(command.VendorId, command.PayToVendor) &&
                   IsValidPaymentVia(command.PymtVia, command.BankAccount, command.BankCity, command.BankName,
                       command.BankNumber) && IsValidCourtesyDelivery(command.CourtesyDelivPrintInd, command.Email,
                       command.FaxNumber, command.Contact1);
        }

        /// <summary>
        ///  Check Valid Make by Dealer Id
        /// </summary>
        /// <param name="dealerId"></param>
        /// <param name="makeCode"></param>
        /// <returns>bool</returns>
        private static bool IsValidMakeByDealerId(string dealerId, string makeCode)
        {

            if ((dealerId.ToUpper().Trim() == "CN" && makeCode.ToUpper().Trim() != "BL") || (dealerId.ToUpper().Trim() != "CN" && makeCode.ToUpper().Trim() == "BL"))
            {

                ErrorMessage(CommandConstant.ErrMessageMakeCodeByDealerId);

                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Check Valid Dealer Id
        /// </summary>
        /// <param name="dealerId"></param>
        /// <returns>bool</returns>
        private static bool IsValidDealer(string dealerId)
        {

            if (!string.IsNullOrEmpty(dealerId))
            {
                return true;
            }
            else
            {
                ErrorMessage(CommandConstant.ErrMessageDealerId);
            }
            return false;
        }
        /// <summary>
        /// Check Valid Country Code
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns>bool</returns>
        private static bool IsValidCountry(string countryCode)
        {

            if (!string.IsNullOrEmpty(countryCode))
            {
                return true;
            }
            else
            {
                ErrorMessage(CommandConstant.ErrMessageCountryCode);
            }
            return false;
        }

        /// <summary>
        /// Check Valid Make Code
        /// </summary>
        /// <param name="makeCode"></param>
        /// <returns>bool</returns>
        private static bool IsValidMake(string makeCode)
        {

            if (!string.IsNullOrEmpty(makeCode))
            {
                return true;
            }
            else
            {
                ErrorMessage(CommandConstant.ErrMessageMakeId);
            }
            return false;
        }

        /// <summary>
        /// Check Valid Dealer Id for Pay to Vendor
        /// </summary>
        /// <param name="vendorId"></param>
        /// <param name="payToVendor"></param>
        /// <returns>bool</returns>
        private static bool IsValidVendorId(string vendorId, string payToVendor)
        {
            if (string.IsNullOrEmpty(vendorId) && !string.IsNullOrEmpty(payToVendor))
            {
                ErrorMessage(CommandConstant.ErrMessageVendorId);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Check bank details based on value of payment via is 'A'
        /// </summary>
        /// <param name="pymtVia"></param>
        /// <param name="bankAccount"></param>
        /// <param name="bankCity"></param>
        /// <param name="bankName"></param>
        /// <param name="bankNumber"></param>
        /// <returns>bool</returns>
        private static bool IsValidPaymentVia(string pymtVia, string bankAccount, string bankCity, string bankName, string bankNumber)
        {
            if (pymtVia == "A" && (string.IsNullOrEmpty(bankAccount) || string.IsNullOrEmpty(bankCity) || string.IsNullOrEmpty(bankName) || string.IsNullOrEmpty(bankNumber)))
            {
                ErrorMessage(CommandConstant.ErrMessagePaymentVia);
                return false;
            }
            return true;
        }
        
        /// <summary>
        ///Validate dealer rating range.
        /// </summary>
        /// <param name="rating"></param>
        /// <returns></returns>
        private static bool IsValidRating(string rating)
        {
            if (!string.IsNullOrEmpty(rating))
            {
                //Validate rating range between 1 to 9.
                if (int.Parse(rating) < 1 || int.Parse(rating) > 9)
                {
                    ErrorMessage(CommandConstant.ErrMessageRatingRange1);
                    return false;
                }
                //Validate rating range between 2 to 9.
                if (int.Parse(rating) < 2 || int.Parse(rating) > 9)
                {
                    ErrorMessage(CommandConstant.ErrMessageRatingRange2);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///  Check Courtesy Delivery
        /// </summary>
        /// <returns></returns>
        private static bool IsValidCourtesyDelivery(string courtesyDelivPrintInd, string email, string faxNumber, string contact1)
        {
            if (!string.IsNullOrEmpty(courtesyDelivPrintInd))
            {
                //Validate contact email when courtesy deliver value is email.
                if (courtesyDelivPrintInd.Trim() == "Email" && string.IsNullOrEmpty(email))
                {
                    ErrorMessage(CommandConstant.ErrMessageEmailOnCourtesyDelivery);
                    return false;
                }

                //Validate contact fax when courtesy deliver value is email.
                if (courtesyDelivPrintInd.Trim() == "Fax" && string.IsNullOrEmpty(faxNumber))
                {
                    ErrorMessage(CommandConstant.ErrMessageFaxOnCourtesyDelivery);
                    return false;
                }

                //Validate contact contact1 when courtesy deliver value is email.
                if (courtesyDelivPrintInd.Trim() == "" && string.IsNullOrEmpty(contact1))
                {
                    ErrorMessage(CommandConstant.ErrMessageContactOnCourtesyDelivery);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Factory for throw exception
        /// </summary>        
        private static void ErrorMessage(string errMessage)
        {
            throw new ArgumentException(errMessage);
        }
    }
}

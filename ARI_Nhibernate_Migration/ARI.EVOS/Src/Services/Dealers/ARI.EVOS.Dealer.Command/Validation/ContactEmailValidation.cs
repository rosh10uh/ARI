using ARI.EVOS.Dealer.Command.Commands;
using ARI.EVOS.Dealer.Command.Interface;
using System;

namespace ARI.EVOS.Dealer.Command.Validation
{
    /// <summary>
    /// This class is responsible for contact email business validation
    /// </summary>
    public class ContactEmailValidation:IContactEmailValidation
    {
        /// <summary>
        /// It is responsible to validate business
        /// </summary>
        /// <param name="commandAdd">ContactEmailCommand</param>
        /// <returns>bool</returns>
        public bool IsValid(ContactEmailCommand commandAdd)
        {
            if (!string.IsNullOrEmpty(commandAdd.CountryCode) && !string.IsNullOrEmpty(commandAdd.MakeCode) &&
                !string.IsNullOrEmpty(commandAdd.DealerId))
            {
                return true;
            }
            else
            {
                ErrorMessage(CommandConstant.ErrMessageCountryMakeDealerId);
            }
            return false;
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

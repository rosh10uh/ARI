using ARI.EVOS.Dealer.Command.Commands;
using ARI.EVOS.Dealer.Command.Interface;
using System;

namespace ARI.EVOS.Dealer.Command.Validation
{
    /// <summary>
    /// This class is responsible for get ready business validation
    /// </summary>
    public class GetReadyValidation : IGetReadyValidation
    {
        public GetReadyValidation()
        {
        }

        /// <summary>
        /// It is responsible to validate business
        /// </summary>
        /// <param name="addGetReadyCommand">DealerNetworkAddCommand</param>
        /// <returns>bool</returns>
        public bool IsValid(AddGetReadyCommand addGetReadyCommand)
        {
            return IsValidGetReady(addGetReadyCommand.DealerId) && IsValidCountry(addGetReadyCommand.CountryCode) && IsValidMake(addGetReadyCommand.MakeCode);
        }

        /// <summary>
        /// It is responsible to validate business
        /// </summary>
        /// <param name="updateGetReadyCommand">DealerNetworkUpdateCommand</param>
        /// <returns>bool</returns>
        public bool IsValid(UpdateGetReadyCommand updateGetReadyCommand)
        {
            return IsValidGetReady(updateGetReadyCommand.DealerId) && IsValidCountry(updateGetReadyCommand.CountryCode) && IsValidMake(updateGetReadyCommand.MakeCode);
        }

        /// <summary>
        /// Check Valid Country Code
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns>bool</returns>
        private bool IsValidCountry(string countryCode)
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
        private bool IsValidMake(string makeCode)
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
        /// Check Valid Dealer Id
        /// </summary>
        /// <param name="dealerId"></param>
        /// <returns>bool</returns>
        private bool IsValidGetReady(string dealerId)
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
        /// Factory for throw exception
        /// </summary>        
        private static void ErrorMessage(string errMessage)
        {
            throw new ArgumentException(errMessage);
        }
    }
}

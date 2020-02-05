using EMP.Management.Command.Commands;
using EMP.Management.Command.Interface;
using System;

namespace EMP.Management.Command.Validation
{
    public class EmployeeValidation : IEmployeeValidation
    {

        public EmployeeValidation()
        {

        }
        public bool IsValid(EmployeeCommand command)
        {
            return IsValidEmployee(command.EmployeeId) && IsValidCountry(command.CountryCode) &&
                 IsValidPaymentVia(command.AccountNumber, command.BankName) &&
                 IsValidCourtesyDelivery(command.Email);
        }

        private bool IsValidPaymentVia(string accountNumber, string bankName)
        {
            if (string.IsNullOrEmpty(accountNumber) || string.IsNullOrEmpty(bankName))
            {
                ErrorMessage(CommandConstant.ErrMessagePaymentVia);
                return false;
            }
            return true;
        }

        private static bool IsValidCourtesyDelivery(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ErrorMessage(CommandConstant.ErrMessageEmailOnCourtesyDelivery);
                return false;
            }

            return true;
        }

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

        private static bool IsValidEmployee(string employeeId)
        {

            if (!string.IsNullOrEmpty(employeeId))
            {
                return true;
            }
            else
            {
                ErrorMessage(CommandConstant.ErrMessageEmployeeId);
            }
            return false;
        }

        private static void ErrorMessage(string errMessage)
        {
            throw new ArgumentException(errMessage);
        }
    }
}

using System;
using Chassis.Domain.Aggregate;

namespace ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject
{
    /// <summary>
    /// This class is used to store dealer bank account details
    /// </summary>
    public class BankAccount : ValueObject<BankAccount>
    {
        public virtual string AccountNumber { get; protected set; }

        protected BankAccount()
        {
        }

        private BankAccount(string accountNumber)
        {
            AccountNumber = accountNumber;
        }

        public static BankAccount Create(string accountNumber)
        {
            var bankAccount = new BankAccount(accountNumber);
            return bankAccount.IsValid() ? bankAccount : throw new Exception(DealerDomainConstant.AccountErrorMessage);
        }
        
        private bool IsValid()
        {
            if (!string.IsNullOrEmpty(AccountNumber))
            {
                return AccountNumber.Length <= 20;
            }

            return true;
        }
        protected override bool EqualsCore(BankAccount bankAccount)
        {
            return AccountNumber == bankAccount.AccountNumber;
        }

        protected override int GetHashCodeCore()
        {
            return base.GetHashCode();
        }
    }
}

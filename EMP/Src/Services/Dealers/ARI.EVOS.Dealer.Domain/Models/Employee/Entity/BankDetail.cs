using Chassis.Domain.Aggregate;

namespace ARI.EVOS.Dealer.Domain.Models.Dealer.Entity
{
    public class BankDetail : Entity<int>
    {
        public virtual string BankName { get; protected set; }
        public virtual string AccountNumber { get; protected set; }
        protected BankDetail()
        {
        }

        private BankDetail(string bankName, string accountNumber)
        {
            BankName = bankName;
            AccountNumber = accountNumber;
        }


        public static BankDetail Create(string bankName, string accountNumber)
        {
            return new BankDetail(bankName, accountNumber);
        }

        public void Update(string bankName, string accountNumber)
        {
            BankName = bankName;
            AccountNumber = accountNumber;
        }
    }
}

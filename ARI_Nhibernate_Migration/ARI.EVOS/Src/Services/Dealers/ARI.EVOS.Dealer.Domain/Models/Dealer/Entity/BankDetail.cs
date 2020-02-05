using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using Chassis.Domain.Aggregate;

namespace ARI.EVOS.Dealer.Domain.Models.Dealer.Entity
{
    /// <summary>
    /// This class is used to store dealer bank details
    /// </summary>
    public class BankDetail : Entity<int>
    {
        public virtual string BankNumber { get; protected set; }
        public virtual BankAccount BankAccount { get; protected set; }
        public virtual string BankName { get; protected set; }
        public virtual BankCity BankCity { get; protected set; }
        protected BankDetail()
        {
        }

        private BankDetail(string bankNumber, BankAccount bankAccount, string bankName, BankCity bankCity)
        {
            BankNumber = bankNumber;
            BankAccount = bankAccount;
            BankName = bankName;
            BankCity = bankCity;
        }


        public static BankDetail Create(string bankNumber, BankAccount bankAccount, string bankName, BankCity bankCity)
        {
            return new BankDetail(bankNumber, bankAccount, bankName, bankCity);
        }

        public void Update(BankAccount bankAccount, BankCity bankCity,string bankNumber, string bankName)
        {
            BankAccount = bankAccount;
            BankCity = bankCity;
            BankNumber = bankNumber;
            BankName = bankName;
        }
    }
}

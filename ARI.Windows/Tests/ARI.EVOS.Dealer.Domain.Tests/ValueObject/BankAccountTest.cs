using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using Xunit;

namespace ARI.EVOS.Dealer.Domain.Tests.Dealer.ValueObject
{
    /// <summary>
    /// Test cases of Bank account value object
    /// </summary>
    public class BankAccountTest
    {
        [Fact]
        public void To_Check_Create_Bank_Account()
        {
            //Arrange
            var bank = new BankAccountMock();
            var bankAccount = BankAccount.Create("12345678901");
            BankAccount.Create("");
            bank.Equals(bankAccount);
            Assert.IsType<BankAccount>(bankAccount);
        }
    }

    public class BankAccountMock : BankAccount
    {
        public BankAccountMock()
        {

        }
        public bool Equals(BankAccount other)
        {
            return base.EqualsCore(other);
        }
    }
}

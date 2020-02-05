using ARI.EVOS.Dealer.Domain.Models.Dealer.Entity;
using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using Xunit;

namespace ARI.EVOS.Dealer.Domain.Tests.Dealer.Entity
{
    /// <summary>
    /// Test cases of BankDetail Entity
    /// </summary>
    public class BankDetailTest
    {
        BankAccount bankAccount;
        BankCity bankCity;

        private static void SetBankDetail(out BankAccount bankAccount, out BankCity bankCity)
        {
            var countryCode = CountryCode.Create("US");
            var makeCode = MakeCode.Create("/L", countryCode);
            var dealerId = DealerId.Create("123");
            bankAccount = BankAccount.Create("123");
            bankCity = BankCity.Create("Valsad", makeCode, dealerId);
        }

        [Fact]
        public void To_Check_Create()
        {
            //Arrange
            SetBankDetail(out bankAccount, out bankCity);
            //Act
            var bankDetail = BankDetail.Create("Test1", bankAccount, "Test3", bankCity);
            //Assert
            Assert.IsType<BankDetail>(bankDetail);
        }

        [Fact]
        public void To_Check_Update()
        {
            //Arrange
            SetBankDetail(out bankAccount, out bankCity);
            //Act
            var bankDetail = BankDetail.Create("Test1", bankAccount, "Test3", bankCity);
            //Assert
            bankDetail.Update(bankAccount, bankCity, "test", "test1");
        }

        
    }
}

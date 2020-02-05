using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using Xunit;

namespace ARI.EVOS.Dealer.Domain.Tests.Dealer.ValueObject
{
    /// <summary>
    /// Test cases of Bank city value object
    /// </summary>
    public class BankCityTest
    {
        MakeCode makeCode;
        DealerId dealerId;
        private static void SetBankCityDetails(out MakeCode makeCode, out DealerId dealerId)
        {
            var countryCode = CountryCode.Create("US");
            makeCode = MakeCode.Create("/L", countryCode);
            dealerId = DealerId.Create("44561");
        }

        [Fact]
        public void To_Check_Create_Bank_City()
        {
            //Arrange
            SetBankCityDetails(out makeCode, out dealerId);
            var bankCity = BankCity.Create("Ontario", makeCode, dealerId);
            Assert.IsType<BankCity>(bankCity);
        }
    }
}

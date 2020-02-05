using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using Xunit;

namespace ARI.EVOS.Dealer.Domain.Tests.Dealer.ValueObject
{
    /// <summary>
    /// Test cases of Country code value object
    /// </summary>
    public class CountryCodeTest
    {
        [Fact]
        public void To_Check_Create_Country_Code()
        {
            //Arrange
            var countryCode = CountryCode.Create("CA");

            Assert.IsType<CountryCode>(countryCode);
        }
    }
}

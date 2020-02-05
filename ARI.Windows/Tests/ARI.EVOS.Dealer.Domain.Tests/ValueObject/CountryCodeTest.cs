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
            var countryMock = new CountryCodeMock();
            var countryCode = CountryCode.Create("CA");
            CountryCode.Create("");
            countryMock.Equals(countryCode);
            Assert.IsType<CountryCode>(countryCode);
        }
    }

    public class CountryCodeMock : CountryCode
    {
        public CountryCodeMock()
        {

        }
        public bool Equals(CountryCode other)
        {
            return base.EqualsCore(other);
        }
    }
}

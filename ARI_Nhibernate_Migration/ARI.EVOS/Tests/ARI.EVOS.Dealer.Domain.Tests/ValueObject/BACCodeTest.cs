using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using Xunit;

namespace ARI.EVOS.Dealer.Domain.Tests.Dealer.ValueObject
{
    /// <summary>
    /// Test cases of BAC Code value object
    /// </summary>
    public class BACCodeTest
    {
        [Fact]
        public void To_Check_Create_BAC_Code()
        {
            //Arrange
            var countryCode = CountryCode.Create("US");
            var makeCode = MakeCode.Create("/L", countryCode);
            var bacCode = BacCode.Create(string.Empty, makeCode);

            Assert.IsType<BacCode>(bacCode);
        }
    }
}

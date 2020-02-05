using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using Xunit;

namespace ARI.EVOS.Dealer.Domain.Tests.Dealer.ValueObject
{
    /// <summary>
    /// Test cases of Fax value object
    /// </summary>
    public class FaxTest
    {
        [Fact]
        public void To_Check_Create_Fax()
        {
            //Arrange
            var countryCode = CountryCode.Create("US");
            var fax = Fax.Create("123456789012", countryCode);

            Assert.IsType<Fax>(fax);
        }
    }
}

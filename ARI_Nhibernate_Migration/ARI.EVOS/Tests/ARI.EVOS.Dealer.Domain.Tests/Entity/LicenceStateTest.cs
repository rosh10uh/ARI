using ARI.EVOS.Dealer.Domain.Models.Dealer.Entity;
using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using Xunit;

namespace ARI.EVOS.Dealer.Domain.Tests.Dealer.Entity
{
    /// <summary>
    /// Test cases of License State Entity
    /// </summary>
    public class LicenceStateTest
    {
        [Fact]
        public void To_Check_Create()
        {
            //Arrange
            var countryCode = CountryCode.Create("Test1");
            var makeCode = MakeCode.Create("Test1", countryCode);
            var dealerId = DealerId.Create("1");

            //Act
            var licenceState = LicenceState.Create(dealerId,countryCode, makeCode, "Test");

            //Assert
            Assert.IsType<LicenceState>(licenceState);
        }
    }
}

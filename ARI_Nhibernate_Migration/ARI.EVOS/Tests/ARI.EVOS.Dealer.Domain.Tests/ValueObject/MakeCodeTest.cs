using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using Xunit;

namespace ARI.EVOS.Dealer.Domain.Tests.Dealer.ValueObject
{
    /// <summary>
    /// Test cases of Make code value object
    /// </summary>
    public class MakeCodeTest
    {
        [Fact]
        public void To_Check_Create_Make_Code()
        {
            //Arrange
            var countryCode = CountryCode.Create("US");
            var makeCode = MakeCode.Create("/L", countryCode);

            Assert.IsType<MakeCode>(makeCode);
        }
    }
}

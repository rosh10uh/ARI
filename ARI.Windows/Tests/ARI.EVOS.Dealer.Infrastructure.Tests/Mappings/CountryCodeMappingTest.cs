using ARI.EVOS.Dealer.Infrastructure.Mappings;
using Xunit;

namespace ARI.EVOS.Dealer.Infrastructure.Tests.Mappings
{
    /// <summary>
    /// Test cases for CountryCodeMapping
    /// </summary>
    public class CountryCodeMappingTest
    {
        [Fact]
        public void To_Check_Country_Code_Mapping()
        {
            //Act
            var result = new CountryCodeMapping();

            //Assert
            Assert.NotNull(result);
        }
    }
}

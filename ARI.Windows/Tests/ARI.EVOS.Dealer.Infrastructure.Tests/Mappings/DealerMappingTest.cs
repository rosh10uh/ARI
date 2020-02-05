using ARI.EVOS.Dealer.Infrastructure.Mappings;
using Xunit;

namespace ARI.EVOS.Dealer.Infrastructure.Tests.Mappings
{
    /// <summary>
    /// Test cases for DealerMapping
    /// </summary>
    public class DealerMappingTest
    {
        [Fact]
        public void To_Check_Dealer_Mapping()
        {
            //Act
            var result = new DealerMapping();

            //Assert
            Assert.NotNull(result);
        }
    }
}

using ARI.EVOS.Dealer.Infrastructure.Mappings;
using Xunit;

namespace ARI.EVOS.Dealer.Infrastructure.Tests.Mappings
{
    /// <summary>
    /// Test cases for DealerIDMapping
    /// </summary>
    public class DealerIDMappingTest
    {
        [Fact]
        public void To_Check_Dealer_ID_Mapping()
        {
            //Act
            var result = new DealerIDMapping();

            //Assert
            Assert.NotNull(result);
        }
    }
}

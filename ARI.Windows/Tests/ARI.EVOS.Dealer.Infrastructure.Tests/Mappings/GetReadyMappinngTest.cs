using ARI.EVOS.Dealer.Infrastructure.Mappings;
using Xunit;

namespace ARI.EVOS.Dealer.Infrastructure.Tests.Mappings
{
    /// <summary>
    /// Test cases for GetReadyMappinng
    /// </summary>
    public class GetReadyMappinngTest
    {
        [Fact]
        public void To_Check_Get_Ready_Mappinng_Test()
        {
            //Act
            var result = new GetReadyMappinng();

            //Assert
            Assert.NotNull(result);
        }
    }
}

using ARI.EVOS.Dealer.Infrastructure.Mappings;
using Xunit;

namespace ARI.EVOS.Dealer.Infrastructure.Tests.Mappings
{
    /// <summary>
    /// Test cases for MakeCodeMapping
    /// </summary>
    public class MakeCodeMappingTest
    {

        [Fact]
        public void To_Check_Make_Code_Mapping()
        {
            //Act
            var result = new MakeCodeMapping();

            //Assert
            Assert.NotNull(result);
        }
    }
}

using ARI.EVOS.Dealer.Query.Queries;
using Moq;
using Xunit;

namespace ARI.EVOS.Dealer.Query.Tests.Queries
{
    /// <summary>
    /// Test case for GetCityFromZipQueryTest
    /// </summary>
    public class GetCityFromZipQueryTest
    {

        [Fact]
        public void To_Check_GetCityFromZipQuery()
        {
            //Act
            var result = new GetCityFromZipQuery(It.IsAny<string>());

            //Assert
            Assert.NotNull(result);
        }
    }
}

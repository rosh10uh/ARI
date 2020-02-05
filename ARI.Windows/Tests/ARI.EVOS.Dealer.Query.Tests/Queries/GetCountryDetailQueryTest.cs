using ARI.EVOS.Dealer.Query.Queries;
using Xunit;

namespace ARI.EVOS.Dealer.Query.Tests.Queries
{
    /// <summary>
    /// Test case for GetCountryDetailQueryTest
    /// </summary>
    public class GetCountryDetailQueryTest
    {
        [Fact]
        public void To_Check_GetCountryDetailQuery()
        {
            //Act
            var result = new GetCountryDetailQuery();

            //Assert
            Assert.NotNull(result);
        }
    }
}

using ARI.EVOS.Dealer.Query.Queries;
using Moq;
using Xunit;

namespace ARI.EVOS.Dealer.Query.Tests.Queries
{
    /// <summary>
    /// Test case for SearchDealersQueryTest
    /// </summary>
    public class SearchDealersQueryTest
    {
        [Fact]
        public void To_Check_SearchDealersQuery()
        {
            //Act
            var result = new SearchDealersQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            //Assert
            Assert.NotNull(result);
        }
    }
}

using ARI.EVOS.Dealer.Query.Queries;
using Xunit;

namespace ARI.EVOS.Dealer.Query.Tests.Queries
{
    /// <summary>
    /// Test case for GetDealersListQueryTest
    /// </summary>
    public class GetDealersListQueryTest
    {
        [Fact]
        public void To_Check_GetDealersListQuery()
        {
            //Act
            var result = new GetDealersListQuery();

            //Assert
            Assert.NotNull(result);
        }
    }
}

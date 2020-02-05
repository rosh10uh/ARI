using ARI.EVOS.Dealer.Query.Queries;
using Xunit;

namespace ARI.EVOS.Dealer.Query.Tests.Queries
{
    /// <summary>
    /// Test case for GetMakeDetailQueryTest
    /// </summary>
    public class GetMakeDetailQueryTest
    {
        [Fact]
        public void To_Check_GetMakeDetailQuery()
        {
            //Act
            var result = new GetMakeDetailQuery();

            //Assert
            Assert.NotNull(result);
        }
    }
}

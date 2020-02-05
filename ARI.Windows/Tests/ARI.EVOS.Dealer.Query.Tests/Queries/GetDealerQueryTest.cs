using ARI.EVOS.Dealer.Query.Queries;
using Moq;
using Xunit;

namespace ARI.EVOS.Dealer.Query.Tests.Queries
{
    /// <summary>
    /// Test case for GetDealerQueryTest
    /// </summary>
    public class GetDealerQueryTest
    {
        [Fact]
        public void To_Check_GetDealerQuery()
        {
            //Act
            var result = new GetDealerQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            //Assert
            Assert.NotNull(result);
        }
    }
}

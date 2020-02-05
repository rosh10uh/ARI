using ARI.EVOS.Dealer.Query.Queries;
using Moq;
using Xunit;

namespace ARI.EVOS.Dealer.Query.Tests.Queries
{
    /// <summary>
    /// Test case for GetUserSecurityCodeQueryTest
    /// </summary>
    public class GetUserSecurityCodeQueryTest
    {
        [Fact]
        public void To_Check_GetUserSecurityCodeQuery()
        {
            //Act
            var result = new GetUserSecurityCodeQuery(It.IsAny<string>());

            //Assert
            Assert.NotNull(result);
        }
    }
}

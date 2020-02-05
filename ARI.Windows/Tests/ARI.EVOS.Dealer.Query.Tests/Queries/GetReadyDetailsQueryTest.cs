using ARI.EVOS.Dealer.Query.Queries;
using Moq;
using Xunit;

namespace ARI.EVOS.Dealer.Query.Tests.Queries
{
    /// <summary>
    /// Test case for GetReadyDetailsQueryTest
    /// </summary>
    public class GetReadyDetailsQueryTest
    {
        [Fact]
        public void To_Check_GetReadyDetailsQuery()
        {
            //Act
            var result = new GetReadyDetails(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            //Assert
            Assert.NotNull(result);
        }
    }
}

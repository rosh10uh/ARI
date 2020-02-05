using ARI.EVOS.Dealer.Query.Queries;
using Moq;
using Xunit;

namespace ARI.EVOS.Dealer.Query.Tests.Queries
{
    /// <summary>
    /// Test case for GetContactEmailDetailQueryTest
    /// </summary>
    public class GetContactEmailDetailQueryTest
    {
        [Fact]
        public void To_Check_GetContactEmailDetailQuery()
        {
            //Act
            var result = new GetContactEmailDetailQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            //Assert
            Assert.NotNull(result);
        }
    }
}

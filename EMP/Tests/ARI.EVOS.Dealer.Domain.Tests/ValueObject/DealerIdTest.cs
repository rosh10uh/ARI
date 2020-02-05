using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using Xunit;

namespace ARI.EVOS.Dealer.Domain.Tests.Dealer.ValueObject
{
    /// <summary>
    /// Test cases of Dealer Id value object
    /// </summary>
    public class DealerIdTest
    {
        [Fact]
        public void To_Check_Create_Dealer_Id()
        {
            //Arrange
            var dealerId = DealerId.Create("12345");

            Assert.IsType<DealerId>(dealerId);
        }
    }
}

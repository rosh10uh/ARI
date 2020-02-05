using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using Xunit;

namespace ARI.EVOS.Dealer.Domain.Tests.Dealer.ValueObject
{
    /// <summary>
    /// Test cases of Dealer rating value object
    /// </summary>
    public class DealerRatingTest
    {
        [Fact]
        public void To_Check_Create_Dealer_Rating()
        {
            //Arrange
            var dealerRating = DealerRating.Create("1");

            Assert.IsType<DealerRating>(dealerRating);
        }
    }
}

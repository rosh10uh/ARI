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
            var dealer = new DealerRatingMock();
            var dealerRating = DealerRating.Create("1");
            dealer.Equals(dealerRating);
            Assert.IsType<DealerRating>(dealerRating);
        }
    }

    public class DealerRatingMock : DealerRating
    {
        public DealerRatingMock()
        {

        }
        public bool Equals(DealerRating other)
        {
            return base.EqualsCore(other);
        }
    }
}

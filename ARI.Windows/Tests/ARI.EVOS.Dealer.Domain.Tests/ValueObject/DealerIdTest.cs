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
            var dealer = new DealerIdMock();
            var dealerId = DealerId.Create("12345");
            dealer.Equals(dealerId);
            dealer.HashCode();
            Assert.IsType<DealerId>(dealerId);
        }


    }
    public class DealerIdMock : DealerId
    {
        public DealerIdMock()
        {

        }
        public bool Equals(DealerId other)
        {
            return base.EqualsCore(other);
        }

        public int HashCode()
        {
            Id = "1";
            return base.GetHashCodeCore();
        }
    }
}

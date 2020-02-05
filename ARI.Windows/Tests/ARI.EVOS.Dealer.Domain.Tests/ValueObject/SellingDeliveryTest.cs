using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using Xunit;

namespace ARI.EVOS.Dealer.Domain.Tests.Dealer.ValueObject
{
    /// <summary>
    /// Test cases of Selling delivery value object
    /// </summary>
    public class SellingDeliveryTest
    {
        [Fact]
        public void To_Check_Create_Selling_Delivery()
        {
            //Arrange
            var sellingDeliveryMock = new SellingDeliveryMock();
            var sellingDelivery = SellingDelivery.Create("S");
            SellingDelivery.Create("");
            sellingDeliveryMock.Equals(sellingDelivery);
            Assert.IsType<SellingDelivery>(sellingDelivery);
        }
    }

    public class SellingDeliveryMock : SellingDelivery
    {
        public SellingDeliveryMock()
        {

        }
        public bool Equals(SellingDelivery other)
        {
            return base.EqualsCore(other);
        }
    }
}

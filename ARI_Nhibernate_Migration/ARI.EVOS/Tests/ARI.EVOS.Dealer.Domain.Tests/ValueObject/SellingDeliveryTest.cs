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
            var sellingDelivery = SellingDelivery.Create("S");

            Assert.IsType<SellingDelivery>(sellingDelivery);
        }
    }
}

using ARI.EVOS.Dealer.Domain.Models.Dealer.Entity;
using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using Xunit;

namespace ARI.EVOS.Dealer.Domain.Tests.Dealer.Entity
{
    /// <summary>
    /// Test cases of Dealer other information Entity
    /// </summary>
    public class DealerOtherInformationTest
    {
        [Fact]
        public void To_Check_Create()
        {
            //Arrange
            var dealerRating = DealerRating.Create("1");
            var paymentVia = PaymentVia.Create("C");

            //Act
            var dealer = DealerOtherInformation.Create(dealerRating, paymentVia);

            //Assert
            Assert.IsType<DealerOtherInformation>(dealer);
        }

        [Fact]
        public void To_Check_Update()
        {
            //Arrange
            var countryCode = CountryCode.Create("US");
            var makeCode = MakeCode.Create("/L", countryCode);
            var dealerRating = DealerRating.Create("5");
            var paymentVia = PaymentVia.Create(string.Empty);
            var bacCode = BacCode.Create(string.Empty, makeCode);
            var sellingDelivery = SellingDelivery.Create("D");
            var minoryIndicator = MinoryIndicator.Create("N");

            //Act
            var dealer = DealerOtherInformation.Create(dealerRating, paymentVia);

            dealer.Update(dealerRating, paymentVia, bacCode, sellingDelivery, minoryIndicator);
            Assert.NotNull(dealer);
        }

        [Fact]
        public void To_Check_Set_Other_Information()
        {
            var dealerRating = DealerRating.Create("1");
            var paymentVia = PaymentVia.Create("C");
            var dealer = DealerOtherInformation.Create(dealerRating, paymentVia);
            dealer.SetOtherInformation("Test1", "Test1", "Test1", "Test1", "Test1", "Test1");
            dealer.SetDealerInfo("Test1", 1, 2, "Test1", "Test1");
            Assert.NotNull(dealer);
        }

        [Fact]
        public void To_Check_Set_Information()
        {
            //Arrange
            var countryCode = CountryCode.Create("US");
            var makeCode = MakeCode.Create("/L", countryCode);
            var bacCode = BacCode.Create(string.Empty, makeCode);
            var sellingDelivery = SellingDelivery.Create("S");
            var minoryIndicator = MinoryIndicator.Create("Y");
            var dealerRating = DealerRating.Create("1");
            var paymentVia = PaymentVia.Create("C");

            //Act
            var dealer = DealerOtherInformation.Create(dealerRating, paymentVia);

            dealer.SetInformation(bacCode, sellingDelivery, minoryIndicator);
            Assert.NotNull(dealer);
        }
    }
}

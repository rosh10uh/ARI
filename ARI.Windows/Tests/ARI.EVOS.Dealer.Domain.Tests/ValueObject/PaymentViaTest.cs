using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using Xunit;

namespace ARI.EVOS.Dealer.Domain.Tests.Dealer.ValueObject
{
    /// <summary>
    /// Test cases of Payment via value object
    /// </summary>
    public class PaymentViaTest
    {
        [Fact]
        public void To_Check_Create_PaymentVia()
        {
            //Arrange
            var paymentVia = PaymentVia.Create("C");
            var paymentMock = new PaymentViaMock();
            paymentMock.Equals(paymentVia);
            Assert.IsType<PaymentVia>(paymentVia);
        }
    }

    public class PaymentViaMock : PaymentVia
    {
        public PaymentViaMock()
        {

        }
        public bool Equals(PaymentVia other)
        {
            return base.EqualsCore(other);
        }
    }
}

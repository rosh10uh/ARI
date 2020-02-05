using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using Xunit;

namespace ARI.EVOS.Dealer.Domain.Tests.Dealer.ValueObject
{
    /// <summary>
    /// Test cases of Minority indicator value object
    /// </summary>
    public class MinorityIndicatorTest
    {
        [Fact]
        public void To_Check_Create_Minority_Indicator()
        {
            //Arrange
            var minoryIndicatorMock = new MinoryIndicatorMock();
            var minoryIndicator = MinoryIndicator.Create("Y");
            MinoryIndicator.Create("");
            minoryIndicatorMock.Equals(minoryIndicator);
            Assert.IsType<MinoryIndicator>(minoryIndicator);
        }
    }

    public class MinoryIndicatorMock : MinoryIndicator
    {
        public MinoryIndicatorMock()
        {

        }
        public bool Equals(MinoryIndicator other)
        {
            return base.EqualsCore(other);
        }
    }
}

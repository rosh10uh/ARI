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
            var minoryIndicator = MinoryIndicator.Create("Y");

            Assert.IsType<MinoryIndicator>(minoryIndicator);
        }
    }
}

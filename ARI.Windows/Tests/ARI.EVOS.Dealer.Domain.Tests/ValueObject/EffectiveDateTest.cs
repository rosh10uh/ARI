using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using System;
using Xunit;

namespace ARI.EVOS.Dealer.Domain.Tests.Dealer.ValueObject
{
    /// <summary>
    /// Test cases of Effective date value object
    /// </summary>
    public class EffectiveDateTest
    {
        [Fact]
        public void To_Check_Create_Effective_Date()
        {
            //Arrange
            var effectiveDate = EffectiveDate.Create(Convert.ToDateTime("12/12/2019"));
            var effectiveDateMock = new EffectiveDateMock();
            effectiveDateMock.Equals(effectiveDate);
            Assert.IsType<EffectiveDate>(effectiveDate);
        }
    }

    public class EffectiveDateMock : EffectiveDate
    {
        public EffectiveDateMock()
        {

        }
        public bool Equals(EffectiveDate other)
        {
            return base.EqualsCore(other);
        }
    }
}

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

            Assert.IsType<EffectiveDate>(effectiveDate);
        }
    }
}

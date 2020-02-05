using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using System;
using Xunit;

namespace ARI.EVOS.Dealer.Domain.Tests.Dealer.ValueObject
{
    /// <summary>
    /// Test cases of Fax value object
    /// </summary>
    public class FaxTest
    {
        [Fact]
        public void To_Check_Create_Fax()
        {
            //Arrange
            var faxMock = new FaxMock();
            var countryCode = CountryCode.Create("US");
            var fax = Fax.Create("123456789012", countryCode);
            Assert.ThrowsAny<ArgumentException>(() => Fax.Create("12345456", countryCode));
            Assert.ThrowsAny<ArgumentException>(() => Fax.Create("12345456", CountryCode.Create("UK")));
            faxMock.Equals(fax);
            Assert.IsType<Fax>(fax);
        }
    }

    public class FaxMock : Fax
    {
        public FaxMock()
        {

        }
        public bool Equals(Fax other)
        {
            return base.EqualsCore(other);
        }
    }
}

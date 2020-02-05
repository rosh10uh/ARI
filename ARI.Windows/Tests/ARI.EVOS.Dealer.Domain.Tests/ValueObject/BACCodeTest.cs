using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using System;
using Xunit;

namespace ARI.EVOS.Dealer.Domain.Tests.Dealer.ValueObject
{
    /// <summary>
    /// Test cases of BAC Code value object
    /// </summary>
    public class BACCodeTest
    {
        [Theory]
        [InlineData("CH")]
        public void To_Check_Create_BAC_Code(string code)
        {
            //Arrange
            var countryCode = CountryCode.Create("US");
            var makeCode = MakeCode.Create("/L", countryCode);
            var bacCode = BacCode.Create(string.Empty, makeCode);
            var make = MakeCode.Create(code, countryCode);
            var bacMock = new BacCodeMock();
            Assert.ThrowsAny<ArgumentException>(() => BacCode.Create(string.Empty, make));
            Assert.ThrowsAny<ArgumentException>(() => BacCode.Create("CA", MakeCode.Create("AA", countryCode)));
            bacMock.Equals(bacCode);
            Assert.IsType<BacCode>(bacCode);
        }
    }

    public class BacCodeMock : BacCode
    {
        public BacCodeMock()
        {

        }
        public bool Equals(BacCode other)
        {
            return base.EqualsCore(other);
        }
    }
}

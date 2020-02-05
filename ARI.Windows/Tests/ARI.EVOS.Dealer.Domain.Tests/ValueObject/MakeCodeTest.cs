using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using Xunit;

namespace ARI.EVOS.Dealer.Domain.Tests.Dealer.ValueObject
{
    /// <summary>
    /// Test cases of Make code value object
    /// </summary>
    public class MakeCodeTest
    {
        [Fact]
        public void To_Check_Create_Make_Code()
        {
            //Arrange
            var makeMock = new MakeCodeMock();
            var countryCode = CountryCode.Create("US");
            var makeCode = MakeCode.Create("/L", countryCode);
            MakeCode.Create(string.Empty, countryCode);
            makeMock.Equals(makeMock);
            Assert.IsType<MakeCode>(makeCode);
        }
    }

    public class MakeCodeMock : MakeCode
    {
        public MakeCodeMock()
        {

        }

        public bool Equals(MakeCode other)
        {
            return base.EqualsCore(other);
        }
    }
}

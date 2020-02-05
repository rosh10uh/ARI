using ARI.EVOS.Dealer.Domain.Models.Dealer.Entity;
using Xunit;
namespace ARI.EVOS.Dealer.Domain.Tests.Dealer.Entity
{
    /// <summary>
    /// Test cases of Vendor Entity
    /// </summary>
    public class VendorTest
    {
        [Fact]
        public void To_Check_Create()
        {
            //Act
            var vendor = Vendor.Create("Test1", "Test2");

            //Assert
            Assert.IsType<Vendor>(vendor);
        }

        [Fact]
        public void To_Check_Update()
        {
            //Act
            var vendor = Vendor.Create("Test1", "Test2");
            vendor.Update("123", "name");
        }
    }
}

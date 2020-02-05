using ARI.EVOS.Dealer.Domain.Models.Dealer.Entity;
using Xunit;

namespace ARI.EVOS.Dealer.Domain.Tests.Dealer.Entity
{
    /// <summary>
    /// Test cases of Address Entity
    /// </summary>
    public class AddressTest
    {
        [Fact]
        public void To_Check_Create()
        {
            var address = Address.Create("Test1","Test2","Test3","Test4","Test5","Test6","Test7");
            Assert.IsType<Address>(address);
        }

        [Fact]
        public void To_Check_Update()
        {
            var address = Address.Create("Test1", "Test2", "Test3", "Test4", "Test5", "Test6", "Test7");
            address.Update("Test11", "Tes2t2", "Test33", "Test44", "Test55", "Test66", "Test77");
        }
    }
}

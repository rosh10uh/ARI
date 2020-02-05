using ARI.EVOS.Dealer.Infrastructure.Mappings;
using Xunit;

namespace ARI.EVOS.Dealer.Infrastructure.Tests.Mappings
{
    /// <summary>
    /// Test cases for ContactEmailMapping
    /// </summary>
    public class ContactEmailMappingTest
    {
        [Fact]
        public void To_Check_Contact_Email_Mapping()
        {
            //Act
            var result = new ContactEmailMapping();

            //Assert
            Assert.NotNull(result);
        }
    }
}

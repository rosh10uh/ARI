using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace ARI.EVOS.Dealer.Query.Tests.Extension
{
    public class QueryServicesExtensionTest
    {
        private readonly Mock<IServiceCollection> _serviceCollection;
        public QueryServicesExtensionTest()
        {
            _serviceCollection = new Mock<IServiceCollection>();
        }

        [Fact]
        public void To_Check_Add_Query_Services()
        {
            _serviceCollection.Verify();
        }
    }
}

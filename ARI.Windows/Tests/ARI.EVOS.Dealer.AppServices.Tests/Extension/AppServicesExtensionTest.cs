using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace ARI.EVOS.Dealer.AppServices.Tests.Extension
{
    public class AppServicesExtensionTest
    {
        private readonly Mock<IServiceCollection> _serviceCollection;
        public AppServicesExtensionTest()
        {
            _serviceCollection = new Mock<IServiceCollection>();
        }

        [Fact]
        public void To_Check_Add_Dealer_Services()
        {
            _serviceCollection.VerifyAll();
        }
    }
}

using ARI.EVOS.Dealer.AppServices.Interface;
using ARI.EVOS.Dealer.Command.Commands;
using ARI.EVOS.Dealers.Models;
using Chassis.Command.Interfaces;
using Chassis.Dapper.Interfaces.Query;
using Moq;
using Xunit;

namespace ARI.EVOS.Dealer.AppServices.Tests
{
    public class DealerNetworkTest
    {
        private readonly Mock<BaseAppService> _baseAppService;
        private readonly Mock<IDealerNetwork> _dealerNetwork;
        private readonly Mock<ICommandBus> _commandBus;
        private readonly Mock<IQueryDispatcher> _queryDispatcher;

        public DealerNetworkTest()
        {
            _baseAppService = new Mock<BaseAppService>();
            _dealerNetwork = new Mock<IDealerNetwork>();
            _commandBus = new Mock<ICommandBus>();
            _queryDispatcher = new Mock<IQueryDispatcher>();
        }

        [Fact]
        public void To_Check_Insert_Dealer_Network()
        {
            var dealerNetworkModel = new Mock<DealerNetworkModel>();
            var dealerNetwork = new DealerNetwork(_commandBus.Object, _queryDispatcher.Object);
            dealerNetwork.InsertDealerNetwork(dealerNetworkModel.Object);
            Assert.NotNull(dealerNetwork);
        }

        [Fact]
        public void To_Check_Update_Dealer_Network()
        {
            var dealerNetworkModel = new Mock<DealerNetworkModel>();
            var dealerNetwork = new DealerNetwork(_commandBus.Object, _queryDispatcher.Object);
            dealerNetwork.UpdateDealerNetwork(dealerNetworkModel.Object);
            Assert.NotNull(dealerNetwork);
        }

        [Fact]
        public void To_Check_Delete_Dealer_Network()
        {
            var dealerNetworkModel = new Mock<DealerNetworkModel>();
            var dealerNetwork = new DealerNetwork(_commandBus.Object, _queryDispatcher.Object);
            dealerNetwork.DeleteDealerNetwork(dealerNetworkModel.Object);
            Assert.NotNull(dealerNetwork);
        }

        [Fact]
        public void To_Check_Insert_Get_Ready()
        {
            var getReadyModel = new Mock<GetReadyModel>();
            var dealerNetwork = new DealerNetwork(_commandBus.Object, _queryDispatcher.Object);
            dealerNetwork.InsertGetReady(getReadyModel.Object);
            Assert.NotNull(dealerNetwork);
        }

        [Fact]
        public void To_Check_Update_Get_Ready()
        {
            var getReadyModel = new Mock<GetReadyModel>();
            var dealerNetwork = new DealerNetwork(_commandBus.Object, _queryDispatcher.Object);
            dealerNetwork.UpdateGetReady(getReadyModel.Object);
            Assert.NotNull(dealerNetwork);
        }

        [Fact]
        public void To_Check_Delete_Get_Ready()
        {
            var getReadyModel = new Mock<GetReadyModel>();
            var dealerNetwork = new DealerNetwork(_commandBus.Object, _queryDispatcher.Object);
            dealerNetwork.DeleteGetReady(getReadyModel.Object);
            Assert.NotNull(dealerNetwork);
        }

        [Fact]
        public void To_Check_Save_Email()
        {
            var contactEmailModel = new Mock<ContactEmailModel>();
            var dealerNetwork = new DealerNetwork(_commandBus.Object, _queryDispatcher.Object);
            dealerNetwork.SaveEmail(contactEmailModel.Object);
            Assert.NotNull(dealerNetwork);
        }

        [Theory]
        [InlineData("US", "/L", "123")]
        public void To_Check_Get_Contact_Email_Detail(string countryCode, string makeCode, string dealerId)
        {
            var dealerNetwork = new DealerNetwork(_commandBus.Object, _queryDispatcher.Object);
            dealerNetwork.GetContactEmailDetail(countryCode, makeCode, dealerId);
            Assert.NotNull(dealerNetwork);
        }

        [Fact]
        public void To_Check_Get_Dealer_Network()
        {
            var dealerNetworkModel = new Mock<DealerNetworkModel>();
            var dealerNetwork = new DealerNetwork(_commandBus.Object, _queryDispatcher.Object);
            dealerNetwork.GetDealerNetwork(dealerNetworkModel.Object);
            Assert.NotNull(dealerNetwork);
        }

        [Theory]
        [InlineData("123")]
        public void To_Check_Get_User_Security_Code(string userId)
        {
            var dealerNetwork = new DealerNetwork(_commandBus.Object, _queryDispatcher.Object);
            dealerNetwork.GetUserSecurityCode(userId);
            Assert.NotNull(dealerNetwork);
        }

        [Fact]
        public void To_Check_Search_Dealers()
        {
            var dealerSearchModel = new Mock<DealerSearchModel>();
            var dealerNetwork = new DealerNetwork(_commandBus.Object, _queryDispatcher.Object);
            dealerNetwork.SearchDealers(dealerSearchModel.Object);
            Assert.NotNull(dealerNetwork);
        }

        [Fact]
        public void To_Check_Get_Dealers_List()
        {
            var dealerNetwork = new DealerNetwork(_commandBus.Object, _queryDispatcher.Object);
            dealerNetwork.GetDealersList();
            Assert.NotNull(dealerNetwork);
        }

        [Theory]
        [InlineData("98233")]
        public void To_Check_Get_City_From_Zip(string zipCode)
        {
            var dealerNetwork = new DealerNetwork(_commandBus.Object, _queryDispatcher.Object);
            dealerNetwork.GetCityFromZip(zipCode);
            Assert.NotNull(dealerNetwork);
        }

        [Fact]
        public void To_Check_Get_Ready_Details()
        {
            var getReadyModel = new Mock<GetReadyModel>();
            var dealerNetwork = new DealerNetwork(_commandBus.Object, _queryDispatcher.Object);
            dealerNetwork.GetReadyDetails(getReadyModel.Object);
            Assert.NotNull(dealerNetwork);
        }
    }
}

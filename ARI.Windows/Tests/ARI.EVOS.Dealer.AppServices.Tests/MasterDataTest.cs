using ARI.EVOS.Common.Models;
using ARI.EVOS.Dealer.AppServices.Interface;
using Chassis.Command.Interfaces;
using Chassis.Dapper.Interfaces.Query;
using CSharpFunctionalExtensions;
using Moq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xunit;

namespace ARI.EVOS.Dealer.AppServices.Tests
{
    public class MasterDataTest
    {
        private readonly Mock<BaseAppService> _baseAppService;
        private readonly Mock<IDealerNetwork> _dealerNetwork;
        private readonly Mock<ICommandBus> _commandBus;
        private readonly Mock<IQueryDispatcher> _queryDispatcher;

        public MasterDataTest()
        {
            _baseAppService = new Mock<BaseAppService>();
            _dealerNetwork = new Mock<IDealerNetwork>();
            _commandBus = new Mock<ICommandBus>();
            _queryDispatcher = new Mock<IQueryDispatcher>();
        }

        [Fact]
        public void To_Check_Get_Country_Detail()
        {
            var masterData = new MasterData(_commandBus.Object, _queryDispatcher.Object);
            masterData.GetCountryDetail();
        }

        [Fact]
        public void To_Check_Get_Make_Detail()
        {
            var masterData = new MasterData(_commandBus.Object, _queryDispatcher.Object);
            masterData.GetMakeDetail();
        }
    }
}

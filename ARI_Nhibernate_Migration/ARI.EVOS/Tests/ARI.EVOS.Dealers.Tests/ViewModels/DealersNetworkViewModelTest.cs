using ARI.EVOS.Common.Models;
using ARI.EVOS.Dealer.AppServices.Interface;
using ARI.EVOS.Dealers.Models;
using ARI.EVOS.Dealers.ViewModels;
using ARI.EVOS.Dealers.Views;
using ARI.EVOS.Infra.Service;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Moq;
using Prism.Events;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xunit;

namespace ARI.EVOS.Dealers.Tests
{
    /// <summary>
    /// This test class is used to validate method implementation of DealerNetwork Viewmodel
    /// </summary>
    public class DealersNetworkViewModelTest
    {
        private readonly Mock<IRegionManager> _regionManager;
        private readonly Mock<IDealerNetwork> _dealerNetwork;
        private readonly Mock<IMasterData> _masterData;
        private readonly Mock<IEventAggregator> _eventAggregator;
        private readonly Mock<ILogger<DealersNetworkViewModel>> _logger;
        private readonly Mock<IContainerProvider> _containerProvider;

        private readonly Mock<IResult> _action;
        private readonly DealersNetworkViewModel _dealersNetworkViewModel;

        public DealersNetworkViewModelTest()
        {
            _masterData = new Mock<IMasterData>();
            _dealerNetwork = new Mock<IDealerNetwork>();
            _regionManager = new Mock<IRegionManager>();
            _eventAggregator = new Mock<IEventAggregator>();
            _logger = new Mock<ILogger<DealersNetworkViewModel>>();
            _containerProvider = new Mock<IContainerProvider>();
            _action = new Mock<IResult>();
            
            var mockCountry = new Mock<ObservableCollection<CountryModel>>();
            var resultCountry = Task.FromResult(Maybe<ObservableCollection<CountryModel>>.From(mockCountry.Object));
            _masterData.Setup(x => x.GetCountryDetail()).Returns(resultCountry);

            var mockMake = new Mock<ObservableCollection<MakeModel>>();
            var resultMake = Task.FromResult(Maybe<ObservableCollection<MakeModel>>.From(mockMake.Object));
            _masterData.Setup(x => x.GetMakeDetail()).Returns(resultMake);

            var mockDealer = new Mock<ObservableCollection<DealerSearchModel>>();
            var resultDealer = Task.FromResult(Maybe<ObservableCollection<DealerSearchModel>>.From(mockDealer.Object));
            _dealerNetwork.Setup(x => x.GetDealersList()).Returns(resultDealer);
            _eventAggregator.Setup(x => x.GetEvent<MessageSentEvent>().Publish(null));

            _dealersNetworkViewModel = new DealersNetworkViewModel(_dealerNetwork.Object, _masterData.Object, _logger.Object, _eventAggregator.Object, _regionManager.Object, _containerProvider.Object);
        }

        [Fact]
        public void To_Check_Country_Change_Command()
        {
            _dealersNetworkViewModel.CountryChangeCommand.Execute();
        }

        [Fact]
        public void To_Check_Insert_Command()
        {
            _dealersNetworkViewModel.DealerNetwork = new DealerNetworkModel() { CountryCode = "US", DealerId = "123", MakeCode = "/L" };
            _dealersNetworkViewModel.PrintCourtesy = new PrintCourtesyModel() { PrintCourtesy = "Fax", Description = "Fax" };
            _dealersNetworkViewModel.TermsOverride = new TermsOverrideModel() { TermsOverride = "Fax", Description = "Fax" };
            var isSuccess = _action.Setup(x => x.IsSuccess).Returns(false);
            var resultSuccess = Task.FromResult(isSuccess);
            _dealerNetwork.Setup(x => x.InsertDealerNetwork(_dealersNetworkViewModel.DealerNetwork)).Returns(Task.FromResult(new Result<string>()));
            _dealersNetworkViewModel.InsertCommand.Execute();
        }

        [Fact]
        public void To_Check_Update_Command()
        {
            _dealersNetworkViewModel.DealerNetwork = new DealerNetworkModel() { CountryCode = "US", DealerId = "123", MakeCode = "/L" };
            _dealersNetworkViewModel.PrintCourtesy = new PrintCourtesyModel() { PrintCourtesy = "Fax", Description = "Fax" };
            _dealersNetworkViewModel.TermsOverride = new TermsOverrideModel() { TermsOverride = "Fax", Description = "Fax" };
            _dealerNetwork.Setup(x => x.UpdateDealerNetwork(_dealersNetworkViewModel.DealerNetwork)).Returns(Task.FromResult(new Result<string>()));
            _dealersNetworkViewModel.UpdateCommand.Execute();
        }

        [Fact]
        public void To_Check_Delete_Command()
        {
            _dealersNetworkViewModel.DealerNetwork = new DealerNetworkModel() { CountryCode = "US", MakeCode = "/L", DealerId = "123" };
            _dealerNetwork.Setup(x => x.DeleteDealerNetwork(It.IsAny<DealerNetworkModel>())).Returns(Task.FromResult(Result.Ok("Test")));
            _dealersNetworkViewModel.DeleteCommand.Execute();
        }

        [Fact]
        public void To_Check_Cancel_Command()
        {
            _dealersNetworkViewModel.CancelCommand.Execute();
        }

        [Fact]
        public void To_Check_Close_Command()
        {
            _regionManager.Setup(x => x.RequestNavigate(It.IsAny<string>(), It.IsAny<string>()));
            _dealersNetworkViewModel.CloseCommand.Execute();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void To_Get_City_Command(bool result)
        {
            _dealerNetwork.Setup(x => x.GetCityFromZip(It.IsAny<string>())).Returns(result ? Task.FromResult(Maybe<DealerNetworkModel>.From(new DealerNetworkModel { City = "Valsad", State = "Gujarat" })) : Task.FromResult(Maybe<DealerNetworkModel>.From(null)));
            _dealersNetworkViewModel.GetCityCommand.Execute();
        }

        [Fact]
        public void To_Check_Insert_Get_Ready()
        {
            _dealersNetworkViewModel.DealerNetwork = new DealerNetworkModel() { CountryCode = "US", MakeCode = "/L", DealerId = "123" };
            _dealersNetworkViewModel.GetReadyModel = new GetReadyModel() { CountryCode = "US", MakeCode = "/L", DealerId = "123", GetReadyCategory = "Cars" };
            _dealerNetwork.Setup(x => x.GetReadyDetails(It.IsAny<GetReadyModel>())).Returns(Task.FromResult(Maybe<ObservableCollection<GetReadyModel>>.From(new ObservableCollection<GetReadyModel>())));
            _dealerNetwork.Setup(x => x.InsertGetReady(It.IsAny<GetReadyModel>())).Returns(Task.FromResult(Result.Ok("Test")));
            _dealersNetworkViewModel.AddGetReadyCommand.Execute();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void To_Check_Row_Single_Click_Command(bool result)
        {
            _dealersNetworkViewModel.RowSingleClickCommand.Execute(result ? new GetReadyModel { GetReadyEffectiveDate = Convert.ToDateTime("18/12/2019"), GetReadyCategory = "Cars" } : new GetReadyModel() { });
        }

        [Fact]
        public void To_Check_Show_GetReady_Details_Command()
        {
            _dealerNetwork.Setup(x => x.GetReadyDetails(It.IsAny<GetReadyModel>())).Returns(Task.FromResult(Maybe<ObservableCollection<GetReadyModel>>.From(new ObservableCollection<GetReadyModel>())));
            _dealersNetworkViewModel.ShowGetReadyDetailsCommand.Execute();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void To_Check_Update_Get_Ready(bool result)
        {
            _dealersNetworkViewModel.DealerNetwork = new DealerNetworkModel() { CountryCode = "US", DealerId = "123", MakeCode = "/L" };
            _dealersNetworkViewModel.GetReadyModel = new GetReadyModel() { CountryCode = "US", DealerId = "123", MakeCode = "/L" };
            _dealerNetwork.Setup(x => x.UpdateGetReady(It.IsAny<GetReadyModel>())).Returns(result ? Task.FromResult(new Result<string>()) : Task.FromResult(Result.Ok(string.Empty)));
            _dealerNetwork.Setup(x => x.GetReadyDetails(It.IsAny<GetReadyModel>())).Returns(Task.FromResult(Maybe<ObservableCollection<GetReadyModel>>.From(new ObservableCollection<GetReadyModel>())));
            _dealersNetworkViewModel.UpdateGetReadyCommand.Execute();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void To_Check_Delete_Get_Ready(bool result)
        {

            _dealersNetworkViewModel.DealerNetwork = new DealerNetworkModel() { CountryCode = "US", MakeCode = "/L", DealerId = "123" };
            _dealersNetworkViewModel.GetReadyModel = new GetReadyModel() { CountryCode = "US", MakeCode = "/L", DealerId = "123", GetReadyCategory = "Cars" };
            _dealerNetwork.Setup(x => x.GetReadyDetails(It.IsAny<GetReadyModel>())).Returns(Task.FromResult(Maybe<ObservableCollection<GetReadyModel>>.From(new ObservableCollection<GetReadyModel>())));
            _dealerNetwork.Setup(x => x.DeleteGetReady(It.IsAny<GetReadyModel>())).Returns(result ? Task.FromResult(Result.Success("Test")) : Task.FromResult(Result.Success(string.Empty)));
            _dealersNetworkViewModel.DeleteGetReadyCommand.Execute();
        }

        [Fact]
        public void To_Check_On_Navigated_From()
        {
            //Arrange
            var navigationAware = new Mock<INavigationAware>();
            var regionNavigationService = new Mock<IRegionNavigationService>();
            var uri = new Mock<Uri>("http://www.foo.com/bar.html");
            _regionManager.Setup(x => x.RequestNavigate("ContentRegion", new Uri(typeof(DealersSearchView).FullName, UriKind.Relative)));
            var navigationContext = new Mock<NavigationContext>(regionNavigationService.Object, uri.Object);
            navigationAware.Setup(x => x.OnNavigatedTo(It.IsAny<NavigationContext>()));
            _dealersNetworkViewModel.OnNavigatedFrom(navigationContext.Object);
        }

        [Fact]
        public void To_Check_On_Navigated_To()
        {
            //Arrange
            var navigationAware = new Mock<INavigationAware>();
            var regionNavigationService = new Mock<IRegionNavigationService>();
            var uri = new Mock<Uri>("http://www.foo.com/bar.html");
            _regionManager.Setup(x => x.RequestNavigate("ContentRegion", new Uri(typeof(DealersSearchView).FullName, UriKind.Relative)));
            var navigationContext = new Mock<NavigationContext>(regionNavigationService.Object, uri.Object);
            navigationAware.Setup(x => x.OnNavigatedTo(It.IsAny<NavigationContext>()));

            //Act
            _dealersNetworkViewModel.OnNavigatedTo(navigationContext.Object);
        }
    }
}
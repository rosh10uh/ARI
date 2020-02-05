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
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xunit;

namespace ARI.EVOS.Dealers.Tests.ViewModels
{
    /// <summary>
    /// This test class is used to validate method implementation of Dealer Search viewmodel
    /// </summary>
    public class DealersSearchViewModelTest
    {
        private readonly Mock<IRegionManager> _regionManager;
        private readonly Mock<IDealerNetwork> _dealerNetwork;
        private readonly Mock<IMasterData> _masterData;
        private readonly Mock<IEventAggregator> _eventAggregator;
        private readonly Mock<ILogger<DealersSearchViewModel>> _logger;
        private readonly DealersSearchViewModel _dealersSearchViewModel;


        public DealersSearchViewModelTest()
        {
            _masterData = new Mock<IMasterData>();
            _dealerNetwork = new Mock<IDealerNetwork>();
            _regionManager = new Mock<IRegionManager>();
            _eventAggregator = new Mock<IEventAggregator>();
            _logger = new Mock<ILogger<DealersSearchViewModel>>();

            var mockContry = new Mock<ObservableCollection<CountryModel>>();
            var resultContry = Task.FromResult(Maybe<ObservableCollection<CountryModel>>.From(mockContry.Object));
            _masterData.Setup(x => x.GetCountryDetail()).Returns(resultContry);

            var mockMake = new Mock<ObservableCollection<MakeModel>>();
            var resultMake = Task.FromResult(Maybe<ObservableCollection<MakeModel>>.From(mockMake.Object));
            _masterData.Setup(x => x.GetMakeDetail()).Returns(resultMake);

            var mockDealer = new Mock<ObservableCollection<DealerSearchModel>>();
            var resultDealer = Task.FromResult(Maybe<ObservableCollection<DealerSearchModel>>.From(mockDealer.Object));
            _dealerNetwork.Setup(x => x.GetDealersList()).Returns(resultDealer);

            _eventAggregator.Setup(x => x.GetEvent<MessageSentEvent>().Publish(null));
            _dealersSearchViewModel = new DealersSearchViewModel(_dealerNetwork.Object, _masterData.Object, _regionManager.Object, _eventAggregator.Object, _logger.Object);

        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void To_Check_Search_Command(bool value)
        {
            var mockDealerSearch = new Mock<ObservableCollection<DealerSearchModel>>();
            if (value)
                _dealerNetwork.Setup(x => x.SearchDealers(It.IsAny<DealerSearchModel>())).Returns(Task.FromResult(getDealerSearchModel()));
            else
            {
                var resultDealerSearch = Task.FromResult(Maybe<ObservableCollection<DealerSearchModel>>.From(mockDealerSearch.Object));
                _dealerNetwork.Setup(x => x.SearchDealers(It.IsAny<DealerSearchModel>())).Returns(resultDealerSearch);
            }
            _dealersSearchViewModel.SearchCommand.Execute();
        }

        [Fact]
        public void To_Check_Exit_Command()
        {
            _dealersSearchViewModel.ExitCommand.Execute();
        }


        [Fact]
        public void To_Check_Select_Dealer_Command()
        {
            //Arrange
            DealerSearchModel dealer = new DealerSearchModel();
            _regionManager.Setup(x => x.RequestNavigate("ContentRegion", new Uri(typeof(DealersNetworkView).FullName, UriKind.Relative)));

            //Act
            _dealersSearchViewModel.SelectDealerCommand.Execute(dealer);
        }

        [Fact]
        public void To_Check_Clear_Selection_Command()
        {
            _dealersSearchViewModel.ClearSelectionCommand.Execute();
        }

        [Fact]
        public void To_Check_Country_Change_Command()
        {
            _dealersSearchViewModel.CountryChangeCommand.Execute();
        }

        [Fact]
        public void To_Check_On_Navigated_To()
        {
            //Arrange
            var navigationAware = new Mock<INavigationAware>();
            var regionNavigationService = new Mock<IRegionNavigationService>();
            var uri = new Mock<Uri>("http://www.foo.com/bar.html");
            _regionManager.Setup(x => x.RequestNavigate("ContentRegion", new Uri(typeof(DealersNetworkView).FullName, UriKind.Relative)));
            var navigationContext = new Mock<NavigationContext>(regionNavigationService.Object, uri.Object);
            navigationAware.Setup(x => x.OnNavigatedTo(It.IsAny<NavigationContext>()));

            //Act
            _dealersSearchViewModel.OnNavigatedTo(navigationContext.Object);
        }

        [Fact]
        public void To_Check_Is_Navigation_Target()
        {
            var navigationAware = new Mock<INavigationAware>();
            var regionNavigationService = new Mock<IRegionNavigationService>();
            var uri = new Mock<Uri>("http://www.foo.com/bar.html");
            _regionManager.Setup(x => x.RequestNavigate("ContentRegion", new Uri(typeof(DealersNetworkView).FullName, UriKind.Relative)));
            var navigationContext = new Mock<NavigationContext>(regionNavigationService.Object, uri.Object);
            navigationAware.Setup(x => x.OnNavigatedTo(It.IsAny<NavigationContext>()));
            _dealersSearchViewModel.IsNavigationTarget(navigationContext.Object);
        }

        [Fact]
        public void To_Check_On_Navigated_From()
        {
            //Arrange
            var navigationAware = new Mock<INavigationAware>();
            var regionNavigationService = new Mock<IRegionNavigationService>();
            var uri = new Mock<Uri>("http://www.foo.com/bar.html");
            _regionManager.Setup(x => x.RequestNavigate("ContentRegion", new Uri(typeof(DealersNetworkView).FullName, UriKind.Relative)));
            var navigationContext = new Mock<NavigationContext>(regionNavigationService.Object, uri.Object);
            navigationAware.Setup(x => x.OnNavigatedTo(It.IsAny<NavigationContext>()));

            //Act
            _dealersSearchViewModel.OnNavigatedFrom(navigationContext.Object);
        }

        [Fact]
        public void To_Check_Add_Dealer_Command()
        {
            _dealersSearchViewModel.AddDealerCommand.Execute();
        }

        private Maybe<ObservableCollection<DealerSearchModel>> getDealerSearchModel()
        {
            return new ObservableCollection<DealerSearchModel>()
            {
                new DealerSearchModel
                {
                    City = "Valsad",
                    CountryCode = "US",
                    DealerId = "156",
                    MakeCode = "Cjay",
                    DealerComments = "testing",
                    DealerRating = "6",
                    SellingDelivInd = "A",
                    State = "Gujarat",
                    VendorId = "1",
                    Make = "test",
                    VendorName = "Test",
                    ZipCode = "396001"
                }
            };
        }
    }
}

using ARI.EVOS.Dealers.Views;
using ARI.EVOS.Infra;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ARI.EVOS.Dealers
{
    /// <summary>
    /// This class contains Dealer Search, Dealer Network regions.
    /// </summary>
    public class DealerModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(DealersSearchView)); 
            // If you want to load dealer network view first then unable below line
            //regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(DealersNetworkView));                      
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<DealersSearchView>();
            containerRegistry.RegisterForNavigation<DealersNetworkView>();
        }
    }
}

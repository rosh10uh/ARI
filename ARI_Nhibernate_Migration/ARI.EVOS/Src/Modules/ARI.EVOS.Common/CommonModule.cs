using ARI.EVOS.Common.Views;
using ARI.EVOS.Infra;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ARI.EVOS.Common
{
    /// <summary>
    /// This class contains menu bar and statusbar regions.
    /// </summary>
    public class CommonModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionNames.BottomRegion, typeof(StatusbarView));
            regionManager.RegisterViewWithRegion(RegionNames.TopRegion, typeof(MenuView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<StatusbarView>();
            containerRegistry.RegisterForNavigation<MenuView>();
        }
    }
}

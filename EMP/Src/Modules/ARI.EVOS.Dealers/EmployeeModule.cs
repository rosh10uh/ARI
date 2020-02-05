using ARI.EVOS.Dealers.Views;
using ARI.EVOS.Infra;
using EMP.Management.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace EMP.Management
{
    public class EmployeeModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(EmployeeListView));
            //regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(EmployeeFormView));

            // If you want to load dealer network view first then unable below line
            //regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(DealersNetworkView));                      
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<EmployeeFormView>();
            containerRegistry.RegisterForNavigation<EmployeeListView>();
        }
    }
}

using ARI.EVOS.Common.Models;
using Prism.Mvvm;

namespace ARI.EVOS.Common.ViewModels
{
    /// <summary>
    /// Map with common UI screen.(MenuView)
    /// </summary>
    public class MenuViewModel : BindableBase
    {
        private MenuModel _menu;
        public MenuModel Menu
        {
            get { return _menu; }
            set
            {
                SetProperty(ref _menu, value);
            }
        }
    }
}

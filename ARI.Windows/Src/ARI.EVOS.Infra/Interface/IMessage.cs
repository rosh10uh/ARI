using System.Windows;

namespace ARI.EVOS.Infra.Interface
{
    /// <summary>
    /// This interface is used for display message box result
    /// </summary>
    public interface IMessage
    {
        MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon);
    }
}

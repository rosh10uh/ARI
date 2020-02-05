using ARI.EVOS.Infra.Interface;
using System.Windows;

namespace ARI.EVOS.Infra.Service
{
    /// <summary>
    /// This class is used to display messagebox
    /// </summary>
    public class Message : IMessage
    {
        /// <summary>
        /// This method is used to show message
        /// </summary>
        /// <param name="messageBoxText"></param>
        /// <param name="caption"></param>
        /// <param name="button"></param>
        /// <param name="icon"></param>
        /// <returns></returns>
        public virtual MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            return MessageBox.Show(messageBoxText, caption, button, icon);
        }
    }
}

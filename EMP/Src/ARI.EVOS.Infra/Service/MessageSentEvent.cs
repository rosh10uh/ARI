using Prism.Events;

namespace ARI.EVOS.Infra.Service
{
    /// <summary>
    /// Publish and Subscribe event which used to display message into status bar.
    /// </summary>
    public class MessageSentEvent : PubSubEvent<string>
    {
    }
}

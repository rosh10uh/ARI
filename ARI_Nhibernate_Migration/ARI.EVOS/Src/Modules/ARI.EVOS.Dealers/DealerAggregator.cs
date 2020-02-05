using ARI.EVOS.Dealers.Models;
using Prism.Events;

namespace ARI.EVOS.Dealers
{
    /// <summary>
    /// This class is used to publish and subscribe event which used to pass dealer network model to another view.
    /// </summary>
    public class DealerAggregator : PubSubEvent<DealerNetworkModel>
    {
    }
}

using JetBrains.Annotations;

namespace Vostok.ServiceDiscovery.Abstractions
{
    [PublicAPI]
    public interface IServiceBeacon
    {
        void Start();

        void Stop();
    }
}

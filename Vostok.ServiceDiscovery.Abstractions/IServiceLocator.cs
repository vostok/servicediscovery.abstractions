using JetBrains.Annotations;

namespace Vostok.ServiceDiscovery.Abstractions
{
    [PublicAPI]
    public interface IServiceLocator
    {
        [CanBeNull]
        IServiceTopology Locate([NotNull] string environment, [NotNull] string service);
    }
}
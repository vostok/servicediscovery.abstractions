using JetBrains.Annotations;

namespace Vostok.ServiceDiscovery.Abstractions
{
    public interface IEnvironmentInfo
    {
        [NotNull]
        string Environment { get; }

        [CanBeNull]
        string ParentEnvironment { get; }

        [NotNull]
        IEnvironmentInfoProperties Properties { get; }
    }
}
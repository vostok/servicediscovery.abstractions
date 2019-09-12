using System.Collections.Generic;
using JetBrains.Annotations;

namespace Vostok.ServiceDiscovery.Abstractions
{
    /// <inheritdoc cref="IServiceTopologyProperties"/>
    [PublicAPI]
    public interface IEnvironmentInfoProperties : IReadOnlyDictionary<string, string>
    {
        /// <inheritdoc cref="IServiceTopologyProperties.Set"/>
        [NotNull]
        IEnvironmentInfoProperties Set([NotNull] string key, [NotNull] string value);

        /// <inheritdoc cref="IServiceTopologyProperties.Remove"/>
        [NotNull]
        IEnvironmentInfoProperties Remove([NotNull] string key);
    }
}
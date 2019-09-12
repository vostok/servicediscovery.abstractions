using System.Collections.Generic;
using JetBrains.Annotations;

namespace Vostok.ServiceDiscovery.Abstractions
{
    /// <inheritdoc cref="IServiceTopologyProperties"/>
    [PublicAPI]
    public interface IApplicationInfoProperties : IReadOnlyDictionary<string, string>
    {
        /// <inheritdoc cref="IServiceTopologyProperties.Set"/>
        [NotNull]
        IApplicationInfoProperties Set([NotNull] string key, [NotNull] string value);

        /// <inheritdoc cref="IServiceTopologyProperties.Remove"/>
        [NotNull]
        IApplicationInfoProperties Remove([NotNull] string key);
    }
}
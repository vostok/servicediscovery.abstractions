using System.Collections.Generic;
using JetBrains.Annotations;

namespace Vostok.ServiceDiscovery.Abstractions
{
    /// <inheritdoc cref="IServiceTopologyProperties"/>
    [PublicAPI]
    public interface IApplicationInfoProperties : IReadOnlyDictionary<string, string>
    {
        /// <inheritdoc cref="IServiceTopologyProperties.Set"/>
        [Pure]
        [NotNull]
        IApplicationInfoProperties Set([NotNull] string key, [NotNull] string value);

        /// <inheritdoc cref="IServiceTopologyProperties.Remove"/>
        [Pure]
        [NotNull]
        IApplicationInfoProperties Remove([NotNull] string key);
    }
}
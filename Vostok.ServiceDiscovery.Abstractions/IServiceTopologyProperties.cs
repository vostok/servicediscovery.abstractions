using System.Collections.Generic;
using JetBrains.Annotations;

namespace Vostok.ServiceDiscovery.Abstractions
{
    /// <summary>
    /// <para>Represents an immutable snapshot of arbitrary key-value properties associated with application in service discovery registry.</para>
    /// <para>These properties are typically used as a convenient carrier populated and used by various extensions.</para>
    /// </summary>
    [PublicAPI]
    public interface IServiceTopologyProperties : IReadOnlyDictionary<string, string>
    {
        /// <summary>
        /// <para>Returns a new instance of <see cref="IServiceTopologyProperties"/> with a property with given <paramref name="key"/> and <paramref name="value"/>.</para>
        /// <para>Current instance is not modified.</para>
        /// <para>Existing property may get overwritten when using this method.</para>
        /// </summary>
        [Pure]
        [NotNull]
        IServiceTopologyProperties Set([NotNull] string key, [NotNull] string value);

        /// <summary>
        /// <para>Returns a new instance of <see cref="IServiceTopologyProperties"/> without a property with given <paramref name="key"/>.</para>
        /// <para>Current instance is not modified.</para>
        /// </summary>
        [Pure]
        [NotNull]
        IServiceTopologyProperties Remove([NotNull] string key);
    }
}
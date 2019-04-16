using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Vostok.ServiceDiscovery.Abstractions
{
    /// <summary>
    /// Represents the topology of an application registered in service discovery system.
    /// </summary>
    [PublicAPI]
    public interface IServiceTopology
    {
        /// <summary>
        /// <para>Returns addresses of currently registered application replicas.</para>
        /// <para>May be empty if no replicas are currently registered.</para>
        /// </summary>
        [NotNull]
        IReadOnlyList<Uri> Replicas { get; }

        /// <summary>
        /// <para>Returns arbitrary key-value properties associated with application in service discovery registry.</para>
        /// <para>This properties is typically used as a convenient carrier populated and used by various extensions.</para>
        /// </summary>
        [NotNull]
        IReadOnlyDictionary<string, string> Properties { get; }
    }
}
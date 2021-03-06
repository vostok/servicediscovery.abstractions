﻿using System;
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
        /// See <see cref="IServiceTopologyProperties"/>.
        /// </summary>
        [NotNull]
        IServiceTopologyProperties Properties { get; }
    }
}
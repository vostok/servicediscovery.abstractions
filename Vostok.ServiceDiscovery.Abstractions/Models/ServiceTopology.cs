using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Vostok.ServiceDiscovery.Abstractions.Models
{
    public class ServiceTopology : IServiceTopology
    {
        private ServiceTopology([NotNull] IReadOnlyList<Uri> replicas, [CanBeNull] IReadOnlyDictionary<string, string> properties)
        {
            Replicas = replicas;

            Properties = new ServiceTopologyProperties(properties);
        }

        public static ServiceTopology Build([CanBeNull] IReadOnlyList<Uri> replicas, [CanBeNull] IReadOnlyDictionary<string, string> properties)
        {
            return replicas == null
                ? null
                : new ServiceTopology(replicas, properties);
        }

        /// <inheritdoc />
        public IReadOnlyList<Uri> Replicas { get; }

        /// <inheritdoc />
        public IServiceTopologyProperties Properties { get; }
    }
}
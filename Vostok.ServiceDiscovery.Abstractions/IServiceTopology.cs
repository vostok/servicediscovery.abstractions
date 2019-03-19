using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Vostok.ServiceDiscovery.Abstractions
{
    [PublicAPI]
    public interface IServiceTopology
    {
        [NotNull]
        IReadOnlyList<Uri> Replicas { get; }

        [NotNull]
        IReadOnlyDictionary<string, string> Data { get; }
    }
}
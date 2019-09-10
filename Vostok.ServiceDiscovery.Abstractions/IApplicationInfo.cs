using System.Collections.Generic;
using JetBrains.Annotations;

namespace Vostok.ServiceDiscovery.Abstractions
{
    public interface IApplicationInfo
    {
        [NotNull]
        string Environment { get; }

        [NotNull]
        string Application { get; }

        [NotNull]
        IReadOnlyDictionary<string, string> Properties { get; }
    }
}
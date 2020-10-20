using System.Collections.Generic;
using JetBrains.Annotations;
using Vostok.ServiceDiscovery.Abstractions.Models;

namespace Vostok.ServiceDiscovery.Abstractions
{
    /// <summary>
    /// Represents information about an instance of application registered in service discovery system.
    /// </summary>
    [PublicAPI]
    public interface IReplicaInfo
    {
        /// <summary>
        /// Application environment.
        /// </summary>
        [NotNull]
        string Environment { get; }

        /// <summary>
        /// Application name.
        /// </summary>
        [NotNull]
        string Application { get; }

        /// <summary>
        /// Application replica id.
        /// </summary>
        [NotNull]
        string Replica { get; }

        /// <summary>
        /// Application properties.
        /// </summary>
        [NotNull]
        IReadOnlyDictionary<string, string> Properties { get; }
        
        /// <summary>
        /// Application tags.
        /// </summary>
        [NotNull]
        TagCollection Tags { get; }
    }
}
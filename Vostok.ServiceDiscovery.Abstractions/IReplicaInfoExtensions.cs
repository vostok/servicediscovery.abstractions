using System;
using JetBrains.Annotations;

namespace Vostok.ServiceDiscovery.Abstractions
{
    [PublicAPI]
    public static class IReplicaInfoExtensions
    {
        /// <summary>
        /// Tries to parse absolute url from <see cref="IReplicaInfo.Replica"/>.
        /// </summary>
        public static bool TryGetUrl([NotNull] this IReplicaInfo replicaInfo, out Uri result) =>
            Uri.TryCreate(replicaInfo.Replica, UriKind.Absolute, out result);
    }
}
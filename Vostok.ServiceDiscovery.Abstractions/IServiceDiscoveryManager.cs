using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Vostok.ServiceDiscovery.Abstractions
{
    [PublicAPI]
    public interface IServiceDiscoveryManager
    {
        [NotNull]
        Task<IReadOnlyList<string>> GetAllEnvironmentsAsync();

        [NotNull]
        Task<IReadOnlyList<string>> GetAllApplicationsAsync(string environment);

        [NotNull]
        Task<string> GetParentZoneAsync(string environment);

        Task<bool> TryAddNode(string environment, string parent);

        Task<bool> TryDeleteNode(string environment);

        Task<bool> AddToBlacklist(string environment, string topologyName, Uri replicaUri);

        Task<bool> RemoveFromBlacklist(string environment, string topologyName, Uri replicaUri);

        Task<bool> SetExternalUrl(string environment, string application, Uri externalUrl);
    }
}

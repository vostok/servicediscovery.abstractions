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

        [CanBeNull]
        Task<IEnvironmentInfo> GetEnvironmentAsync([NotNull] string environment);

        Task<bool> TryCreateEnvironmentAsync([NotNull] IEnvironmentInfo environment);

        Task<bool> TryDeleteEnvironmentAsync([NotNull] string environment);

        Task<bool> TryUpdateEnvironmentPropertiesAsync([NotNull] string environment, [NotNull] Func<IEnvironmentInfoProperties, IEnvironmentInfoProperties> update);

        Task<bool> TryUpdateEnvironmentParentAsync(string environment, string newParent);

        [NotNull]
        Task<IReadOnlyList<string>> GetAllApplicationsAsync(string environment);

        [CanBeNull]
        Task<IApplicationInfo> GetApplicationAsync(string environment, string application);

        Task<bool> TryUpdateApplicationPropertiesAsync(string environment, string application, Func<IApplicationInfoProperties, IApplicationInfoProperties> updateFunc);

        Task<bool> TryCreatePermanentReplicaAsync(IReplicaInfo replica);

        // CR(kungurtsev): check that zookeeper node is persistent before delete.
        Task<bool> TryDeletePermanentReplicaAsync(IReplicaInfo replica);
    }
}
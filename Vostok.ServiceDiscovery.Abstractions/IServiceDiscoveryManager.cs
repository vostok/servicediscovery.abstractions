using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Vostok.ServiceDiscovery.Abstractions
{
    [PublicAPI]
    public interface IServiceDiscoveryManager
    {
        [ItemNotNull]
        Task<IReadOnlyList<string>> GetAllEnvironmentsAsync();

        [ItemNotNull]
        Task<IReadOnlyList<string>> GetAllApplicationsAsync([NotNull] string environment);

        [ItemNotNull]
        Task<IReadOnlyList<string>> GetAllReplicasAsync([NotNull] string environment, [NotNull] string application);

        [ItemCanBeNull]
        Task<IEnvironmentInfo> GetEnvironmentAsync([NotNull] string environment);

        [ItemCanBeNull]
        Task<IApplicationInfo> GetApplicationAsync([NotNull] string environment, [NotNull] string application);

        [ItemCanBeNull]
        Task<IReplicaInfo> GetReplicaAsync([NotNull] string environment, [NotNull] string application, [NotNull] string replica);

        Task<bool> TryCreateEnvironmentAsync([NotNull] IEnvironmentInfo environment);

        Task<bool> TryDeleteEnvironmentAsync([NotNull] string environment);

        Task<bool> TryUpdateEnvironmentPropertiesAsync([NotNull] string environment, [NotNull] Func<IEnvironmentInfoProperties, IEnvironmentInfoProperties> update);

        Task<bool> TryUpdateEnvironmentParentAsync([NotNull] string environment, [NotNull] string newParent);

        Task<bool> TryUpdateApplicationPropertiesAsync([NotNull] string environment, [NotNull] string application, [NotNull] Func<IApplicationInfoProperties, IApplicationInfoProperties> updateFunc);

        Task<bool> TryCreatePermanentReplicaAsync([NotNull] IReplicaInfo replica);

        Task<bool> TryDeletePermanentReplicaAsync([NotNull] string environment, [NotNull] string application, [NotNull] string replica);
    }
}
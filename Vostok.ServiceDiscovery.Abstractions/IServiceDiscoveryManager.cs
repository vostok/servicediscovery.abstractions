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

        [CanBeNull]
        Task<IEnvironmentInfo> GetEnvironmentAsync(string environment);

        [CanBeNull]
        Task<IApplicationInfo> GetApplicationAsync(string environment, string application);

        Task<bool> TryAddEnvironmentAsync(IEnvironmentInfo environment);

        Task<bool> TryDeleteEnvironmentAsync(string environment);

        Task<bool> TryUpdateEnvironmentPropertiesAsync(string environment, Func<IServiceTopologyProperties, IServiceTopologyProperties> updateFunc); 

        Task<bool> TryUpdateApplicationPropertiesAsync(string environment, string application, Func<IServiceTopologyProperties, IServiceTopologyProperties> updateFunc);
    }
}
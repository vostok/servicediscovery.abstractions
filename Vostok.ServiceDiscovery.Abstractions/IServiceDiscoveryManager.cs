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
    }
}

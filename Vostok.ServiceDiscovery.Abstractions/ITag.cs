using JetBrains.Annotations;

namespace Vostok.ServiceDiscovery.Abstractions
{
    /// <summary>
    /// 
    /// </summary>
    [PublicAPI]
    public interface ITag
    {
        /// <summary>
        /// 
        /// </summary>
        [NotNull]
        string Key { get; }
        
        /// <summary>
        /// 
        /// </summary>
        [NotNull]
        string Value { get; }
    }
}
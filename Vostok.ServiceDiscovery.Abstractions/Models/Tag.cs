using System;
using JetBrains.Annotations;

namespace Vostok.ServiceDiscovery.Abstractions.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Tag
    {
        public Tag(string key)
            : this(key, "true")
        {
        }

        public Tag(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentOutOfRangeException(nameof(key), key);
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentOutOfRangeException(nameof(value), value);
            
            Key = key;
            Value = value;
        }

        /// <summary>
        /// 
        /// </summary>
        [NotNull]
        public string Key { get; }
        
        /// <summary>
        /// 
        /// </summary>
        [NotNull]
        public string Value { get; }
    }
}
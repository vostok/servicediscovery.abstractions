using System;
using JetBrains.Annotations;

namespace Vostok.ServiceDiscovery.Abstractions.Models
{
    /// <summary>
    /// 
    /// </summary>
    [PublicAPI]
    public class TagPropertyKey
    {
        public TagPropertyKey([NotNull] string replicaName, [NotNull] string tagKind)
        {
            ReplicaName = replicaName ?? throw new ArgumentNullException(nameof(replicaName));
            TagKind = tagKind ?? throw new ArgumentNullException(nameof(tagKind));
        }
        
        /// <summary>
        /// 
        /// </summary>
        [NotNull]
        public string ReplicaName { get; }
        
        /// <summary>
        /// 
        /// </summary>
        [NotNull]
        public string TagKind { get; }

        public static bool TryParse([NotNull] string input, out TagPropertyKey tagPropertyKey)
        {
            tagPropertyKey = null;
            var split = input.Split(TagsParameterValuesSeparator.ToCharArray());
            if (split.Length != 3 || split[0] != TagsParameterPrefix)
                return false;
            
            tagPropertyKey = new TagPropertyKey(split[1], split[2]);
            return true;
        }

        public override string ToString()
            => TagsParameterPrefix + TagsParameterValuesSeparator + ReplicaName + TagsParameterValuesSeparator + TagKind;
        
        private const string TagsParameterPrefix = "Tags";
        private const string TagsParameterValuesSeparator = "|";
    }
}
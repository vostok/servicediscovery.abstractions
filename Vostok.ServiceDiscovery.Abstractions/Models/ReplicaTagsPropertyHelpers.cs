using System;
using JetBrains.Annotations;

namespace Vostok.ServiceDiscovery.Abstractions.Models
{
    public static class ReplicaTagsPropertyHelpers
    {
        public static bool IsTagsPropertyKey([NotNull] string key)
            => key.StartsWith(ReplicaTagsStoringConstants.TagsParameterPrefix);

        [CanBeNull]
        public static string GetTagPropertyReplicaName([NotNull] string key)
        {
            var tagsParameterPrefixLength = ReplicaTagsStoringConstants.TagsParameterPrefix.Length;
            var tagsParameterValuesSeparatorIndex = key.IndexOf(ReplicaTagsStoringConstants.TagsParameterValuesSeparator, ReplicaTagsStoringConstants.TagsParameterPrefix.Length, StringComparison.InvariantCulture);
            return tagsParameterValuesSeparatorIndex == -1 
                ? key.Substring(tagsParameterPrefixLength) 
                : key.Substring(tagsParameterPrefixLength, tagsParameterValuesSeparatorIndex - tagsParameterPrefixLength);
        }

        [NotNull]
        public static string GetReplicaTagsPropertyKey([NotNull] string replicaName, [NotNull] string propertyKeySuffix)
            => ReplicaTagsStoringConstants.TagsParameterPrefix + replicaName + ReplicaTagsStoringConstants.TagsParameterValuesSeparator + propertyKeySuffix;
    }
}
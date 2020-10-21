using System;
using JetBrains.Annotations;

namespace Vostok.ServiceDiscovery.Abstractions.Models
{
    [PublicAPI]
    public static class TagPropertyHelpers
    {
        private const string TagsParameterPrefix = "Tags:";
        private const string TagsParameterValuesSeparator = ":";

        public static bool IsTagsPropertyKey([NotNull] string key)
            => key.StartsWith(TagsParameterPrefix);

        [CanBeNull]
        public static string ExtractReplicaName([NotNull] string appPropertyName)
        {
            var tagsParameterPrefixLength = TagsParameterPrefix.Length;
            var tagsParameterValuesSeparatorIndex = appPropertyName.IndexOf(TagsParameterValuesSeparator, TagsParameterPrefix.Length, StringComparison.InvariantCulture);
            return tagsParameterValuesSeparatorIndex < 0 
                ? appPropertyName.Substring(tagsParameterPrefixLength) 
                : appPropertyName.Substring(tagsParameterPrefixLength, tagsParameterValuesSeparatorIndex - tagsParameterPrefixLength);
        }

        [NotNull]
        public static string FormatName([NotNull] string replicaName, [NotNull] string tagKind)
            => TagsParameterPrefix + replicaName + TagsParameterValuesSeparator + tagKind;
    }
}
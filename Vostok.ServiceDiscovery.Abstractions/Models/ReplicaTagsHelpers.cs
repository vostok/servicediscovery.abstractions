using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Vostok.ServiceDiscovery.Abstractions.Models
{
    public static class ReplicaTagsHelpers
    {
        [NotNull]
        public static Tag[] Deserialize([NotNull] string value)
            => value.Split(TagsSeparator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Split(TagsKeyValueSeparator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                .Where(t => t.Length == 2)
                .Select(t => new Tag(t[0], t[1]))
                .ToArray();

        [NotNull]
        public static string Serialize([NotNull] Tag[] tags)
        {
            var strings = string.Join(
                TagsKeyValueSeparator,
                tags.Select(x => x.Key + TagsKeyValueSeparator + x.Value));
            return string.Join(TagsSeparator, string.Join(TagsSeparator, strings));
        }
        
        [NotNull]
        public static IEnumerable<Tag> Distinct([NotNull] IEnumerable<Tag> tags) 
        {
            var hashSet = new HashSet<string>();
            foreach (var tag in tags)
            {
                if (hashSet.Add(tag.Key))
                    yield return tag;
            }
        }

        [NotNull]
        public static string GetReplicaTagsPropertyKey([NotNull] string propertyKeySuffix) 
            => TagsParameterPrefix + propertyKeySuffix;
        
        private const string TagsParameterPrefix = "Tags:";
        private const string TagsSeparator = "|";
        private const string TagsKeyValueSeparator = "=";
    }
}
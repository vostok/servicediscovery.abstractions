using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Vostok.ServiceDiscovery.Abstractions
{
    public interface ITag
    {
        string Key { get; }
        string Value { get; }
    }

    public class Tag : ITag
    {
        public Tag(string value)
            : this(value, "true")
        {
        }

        public Tag(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; }
        public string Value { get; }
    }

    internal static class ReplicaTagsPropertiesConstants
    {
        public const string TagsParameterPrefix = "Tags:";
        public const string TagsSeparator = "|";
        public const string TagsKeyValueSeparator = "=";
    }

    public static class ReplicaTagsHelpers
    {
        [NotNull]
        public static ITag[] Deserialize([NotNull] string value)
            => value.Split(ReplicaTagsPropertiesConstants.TagsSeparator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Split(ReplicaTagsPropertiesConstants.TagsKeyValueSeparator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                .Where(t => t.Length == 2)
                .Select(t => (ITag) new Tag(t[0], t[1]))
                .ToArray();

        [NotNull]
        public static string Serialize([NotNull] ITag[] tags)
        {
            var strings = string.Join(
                ReplicaTagsPropertiesConstants.TagsKeyValueSeparator,
                tags.Select(x => x.Key + ReplicaTagsPropertiesConstants.TagsKeyValueSeparator + x.Value));
            return string.Join(ReplicaTagsPropertiesConstants.TagsSeparator, string.Join(ReplicaTagsPropertiesConstants.TagsSeparator, strings));
        }
        
        [NotNull]
        public static IEnumerable<ITag> Distinct([NotNull] IEnumerable<ITag> tags) 
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
            => ReplicaTagsPropertiesConstants.TagsParameterPrefix + propertyKeySuffix;
    }
}
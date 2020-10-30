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
        private const string TagsParameterPrefix = "Tags|";
        private const string TagsParameterValuesSeparator = "|";

        public TagPropertyKey([NotNull] string replicaName, [NotNull] string tagKind)
        {
            ReplicaName = replicaName ?? throw new ArgumentNullException(nameof(replicaName));
            TagKind = tagKind ?? throw new ArgumentNullException(nameof(tagKind));
        }

        public static bool TryParse([NotNull] string input, out TagPropertyKey tagPropertyKey)
        {
            tagPropertyKey = null;
            if (string.IsNullOrEmpty(input) || !input.StartsWith(TagsParameterPrefix))
                return false;

            var tagsParameterValuesSeparatorIndex = input.IndexOf(TagsParameterValuesSeparator, TagsParameterPrefix.Length, StringComparison.InvariantCulture);
            if (tagsParameterValuesSeparatorIndex < 0)
                return false;

            tagPropertyKey = new TagPropertyKey(
                input.Substring(TagsParameterPrefix.Length, tagsParameterValuesSeparatorIndex - TagsParameterPrefix.Length),
                input.Substring(tagsParameterValuesSeparatorIndex + 1, input.Length - tagsParameterValuesSeparatorIndex - 1)
            );

            return true;
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

        public override string ToString()
            => TagsParameterPrefix + ReplicaName + TagsParameterValuesSeparator + TagKind;
    }
}
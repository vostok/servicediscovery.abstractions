using System;
using JetBrains.Annotations;

namespace Vostok.ServiceDiscovery.Abstractions.Models
{
    /// <summary>
    /// <para>Represents replica tags property key that can be used for building tag storing key in application or replica parameters</para>
    /// </summary>
    [PublicAPI]
    public class TagsPropertyKey
    {
        private const string TagsParameterPrefix = "Tags|";
        private const string TagsParameterValuesSeparator = "|";

        public TagsPropertyKey([NotNull] string replicaName, [NotNull] string tagKind)
        {
            ReplicaName = replicaName ?? throw new ArgumentNullException(nameof(replicaName));
            TagKind = tagKind ?? throw new ArgumentNullException(nameof(tagKind));
        }

        public static bool TryParse([NotNull] string input, out TagsPropertyKey tagsPropertyKey)
        {
            tagsPropertyKey = null;
            if (string.IsNullOrEmpty(input) || !input.StartsWith(TagsParameterPrefix))
                return false;

            var tagsParameterValuesSeparatorIndex = input.IndexOf(TagsParameterValuesSeparator, TagsParameterPrefix.Length, StringComparison.InvariantCulture);
            if (tagsParameterValuesSeparatorIndex < 0)
                return false;

            tagsPropertyKey = new TagsPropertyKey(
                input.Substring(TagsParameterPrefix.Length, tagsParameterValuesSeparatorIndex - TagsParameterPrefix.Length),
                input.Substring(tagsParameterValuesSeparatorIndex + 1, input.Length - tagsParameterValuesSeparatorIndex - 1)
            );

            return true;
        }

        [NotNull]
        public string ReplicaName { get; }

        [NotNull]
        public string TagKind { get; }

        public override string ToString()
            => TagsParameterPrefix + ReplicaName + TagsParameterValuesSeparator + TagKind;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Vostok.ServiceDiscovery.Abstractions.Models
{
    public class TagCollection : Dictionary<string, string>
    {
        public TagCollection()
            : base(StringComparer.InvariantCultureIgnoreCase)
        {
        }

        public TagCollection(IDictionary<string, string> tagsCollection)
            : base(tagsCollection, StringComparer.InvariantCultureIgnoreCase)
        {
        }

        public static bool TryParse(string input, out TagCollection tagCollection)
        {
            try
            {
                var dict = input.Split(ReplicaTagsStoringConstants.TagsSeparator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                    .Select(t => t.Split(ReplicaTagsStoringConstants.TagsKeyValueSeparator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                    .Where(t => t.Length == 2)
                    .GroupBy(t => t[0], t => t[1], StringComparer.InvariantCultureIgnoreCase)
                    .ToDictionary(x => x.Key, x => x.First(), StringComparer.InvariantCultureIgnoreCase);
                tagCollection = new TagCollection(dict);
                return true;
            }
            catch
            {
                tagCollection = null;
                return false;
            }
        }

        public void Add([NotNull] Tag tag)
        {
            if (tag == null)
                throw new ArgumentNullException(nameof(tag));

            Add(tag.Key, tag.Value);
        }

        public override string ToString()
        {
            var tags = this.Select(x => x.Key + ReplicaTagsStoringConstants.TagsKeyValueSeparator + x.Value);
            return string.Join(ReplicaTagsStoringConstants.TagsSeparator, tags);
        }

        public IEnumerable<Tag> GetTags()
            => this.Select(tag => new Tag(tag.Key, tag.Value));
    }
}
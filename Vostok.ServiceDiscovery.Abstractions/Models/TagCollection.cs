using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Vostok.Commons.Helpers.Comparers;

namespace Vostok.ServiceDiscovery.Abstractions.Models
{
    [PublicAPI]
    public class TagCollection : Dictionary<string, string>
    {
        private const string DefaultValue = "true";
        private const string TagsSeparator = "|";
        private const string TagsKeyValueSeparator = "═"; // U+2550

        public TagCollection()
            : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        public TagCollection(IDictionary<string, string> tagsCollection)
            : base(tagsCollection, StringComparer.OrdinalIgnoreCase)
        {
        }

        public static bool TryParse([CanBeNull] string input, out TagCollection tagCollection)
        {
            if (string.IsNullOrEmpty(input))
            {
                tagCollection = new TagCollection();
                return true;
            }

            var iterator = 0;
            var collection = new TagCollection();
            tagCollection = null;
            while (iterator <= input.Length)
            {
                var nextTagSeparator = input.IndexOf(TagsSeparator, iterator, StringComparison.Ordinal);
                var nextKeyValueSeparator = input.IndexOf(TagsKeyValueSeparator, iterator, StringComparison.Ordinal);
                if (nextKeyValueSeparator == -1)
                    return false;

                var key = input.Substring(iterator, nextKeyValueSeparator - iterator);
                if (collection.ContainsKey(key))
                    return false;

                if (nextTagSeparator == -1)
                {
                    collection[key] = input.Substring(nextKeyValueSeparator + 1, input.Length - nextKeyValueSeparator - 1);
                    break;
                }

                if (nextKeyValueSeparator >= nextTagSeparator)
                    return false;
                collection[key] = input.Substring(nextKeyValueSeparator + 1, nextTagSeparator - nextKeyValueSeparator - 1);
                iterator = nextTagSeparator + 1;
            }

            tagCollection = collection;
            return true;
        }

        public void Add([NotNull] string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            Add(key, DefaultValue);
        }

        public override string ToString()
            => string.Join(TagsSeparator, this.Select(x => x.Key + TagsKeyValueSeparator + x.Value));

        #region Equality members

        public bool Equals(TagCollection other)
            => DictionaryComparer<string, string>.Instance.Equals(this, other);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;

            return Equals((TagCollection)obj);
        }

        public override int GetHashCode()
            => DictionaryComparer<string, string>.Instance.GetHashCode(this);

        #endregion
    }
}
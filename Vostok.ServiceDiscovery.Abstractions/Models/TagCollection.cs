using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Vostok.ServiceDiscovery.Abstractions.Models
{
    [PublicAPI]
    public class TagCollection : IDictionary<string, string>
    {
        private const string DefaultValue = "true";
        private const string TagsSeparator = "|";
        private const string TagsKeyValueSeparator = "═"; // U+2550

        private readonly Dictionary<string, string> dict;

        public TagCollection()
        {
            dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }

        public TagCollection(IDictionary<string, string> tagsCollection)
        {
            dict = new Dictionary<string, string>(tagsCollection, StringComparer.OrdinalIgnoreCase);
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

        public int Count => dict.Count;
        public ICollection<string> Keys => dict.Keys;
        public ICollection<string> Values => dict.Values;

        public void Add([NotNull] string key)
            => Add(key, DefaultValue);

        public override string ToString()
            => string.Join(TagsSeparator, this.Select(x => x.Key + TagsKeyValueSeparator + x.Value));

        public void Clear()
            => dict.Clear();

        public void Add(string key, string value)
        {
            var (k, v) = NormalizeKeyValue(key, value);
            dict.Add(k, v);
        }

        public bool ContainsKey(string key)
            => dict.ContainsKey(key);

        public bool Remove(string key)
            => dict.Remove(key);

        public bool TryGetValue(string key, out string value)
            => dict.TryGetValue(key, out value);

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
            => dict.GetEnumerator();

        public string this[string key]
        {
            get => dict[key];
            set
            {
                var (k, v) = NormalizeKeyValue(key, value);
                dict[k] = v;
            }
        }
        
        #region Explicit methods
        
        bool ICollection<KeyValuePair<string, string>>.IsReadOnly => ((ICollection<KeyValuePair<string, string>>)dict).IsReadOnly;

        void ICollection<KeyValuePair<string, string>>.Add(KeyValuePair<string, string> item)
            => Add(item.Key, item.Value);

        bool ICollection<KeyValuePair<string, string>>.Contains(KeyValuePair<string, string> item)
            => ((ICollection<KeyValuePair<string, string>>)dict).Contains(item);

        void ICollection<KeyValuePair<string, string>>.CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
            => ((ICollection<KeyValuePair<string, string>>)dict).CopyTo(array, arrayIndex);

        bool ICollection<KeyValuePair<string, string>>.Remove(KeyValuePair<string, string> item)
            => ((ICollection<KeyValuePair<string, string>>)dict).Remove(item);

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
        
        #endregion

        private (string, string) NormalizeKeyValue(string key, string value)
        {
            key = key?.Trim() ?? throw new ArgumentNullException();
            value = value?.Trim();

            if (key == "")
                throw new ArgumentOutOfRangeException(key);
            return (key, value);
        }

        #region Equality members

        public bool Equals(TagCollection other)
        {
            foreach (var pair in dict)
            {
                if (!other.TryGetValue(pair.Key, out var yValue) || pair.Value != yValue)
                    return false;
            }

            return true;
        }

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
        {
            unchecked
            {
                var hashCode = 397;
                foreach (var pair in this)
                {
                    hashCode = (hashCode * 397) ^ pair.Key.GetHashCode();
                    hashCode = (hashCode * 397) ^ (pair.Value?.GetHashCode() ?? 0);
                }

                return hashCode;
            }
        }

        #endregion
    }
}
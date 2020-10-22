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
        private const string TagsKeyValueSeparator = "=";
        
        private static readonly DictionaryComparer<string, string> DictionaryComparer = new DictionaryComparer<string, string>(StringComparer.OrdinalIgnoreCase);

        public TagCollection()
            : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        public TagCollection(IDictionary<string, string> tagsCollection)
            : base(tagsCollection, StringComparer.OrdinalIgnoreCase)
        {
        }
        
        public static bool TryParse(string input, out TagCollection tagCollection)
        {
            try
            {
                var iterator = 0;
                tagCollection = new TagCollection();
                while (iterator < input.Length)
                {
                    var nextTagSeparator = input.IndexOf(TagsSeparator, iterator, StringComparison.Ordinal);
                    var nextKeyValueSeparator = input.IndexOf(TagsKeyValueSeparator, iterator, StringComparison.Ordinal);
                    if (nextTagSeparator == -1)
                    {
                        if (nextKeyValueSeparator != -1)
                        {
                            var key = input.Substring(iterator, nextKeyValueSeparator - iterator);
                            var value = input.Substring(nextKeyValueSeparator + 1, input.Length - nextKeyValueSeparator - 1);
                            tagCollection[key] = value;
                        }
                        break;
                    }

                    if (nextKeyValueSeparator != -1 && nextKeyValueSeparator < nextTagSeparator)
                    {
                        var key = input.Substring(iterator, nextKeyValueSeparator - iterator);
                        var value = input.Substring(nextKeyValueSeparator + 1, nextTagSeparator - nextKeyValueSeparator - 1);
                        tagCollection[key] = value;
                    }
                    iterator = nextTagSeparator + 1;
                }
                
                return true;
            }
            catch
            {
                tagCollection = null;
                return false;
            }
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
            => DictionaryComparer.Equals(this, other);

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
            => DictionaryComparer.GetHashCode(this);

        #endregion
    }
}
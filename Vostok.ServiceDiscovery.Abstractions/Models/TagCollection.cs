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
                // CR(iloktionov): Less garbage please :)

                var dict = input.Split(TagsSeparator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                    .Select(t => t.Split(TagsKeyValueSeparator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
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
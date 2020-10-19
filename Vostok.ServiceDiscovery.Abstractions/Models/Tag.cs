namespace Vostok.ServiceDiscovery.Abstractions.Models
{
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
}
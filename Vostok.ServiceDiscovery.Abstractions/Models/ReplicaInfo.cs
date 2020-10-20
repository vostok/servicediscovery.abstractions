using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Vostok.ServiceDiscovery.Abstractions.Models
{
    public class ReplicaInfo : IReplicaInfo
    {
        private readonly Dictionary<string, string> properties;

        public ReplicaInfo([NotNull] string environment, [NotNull] string application, [NotNull] string replica, [CanBeNull] Dictionary<string, string> properties = null, [CanBeNull] Tag[] tags = null)
        {
            if (string.IsNullOrWhiteSpace(environment))
                throw new ArgumentOutOfRangeException(nameof(environment), environment);
            if (string.IsNullOrWhiteSpace(application))
                throw new ArgumentOutOfRangeException(nameof(application), application);
            if (string.IsNullOrWhiteSpace(replica))
                throw new ArgumentOutOfRangeException(nameof(replica), replica);

            this.properties = properties ?? new Dictionary<string, string>();
            Tags = ReplicaTagsHelpers.Distinct(tags ?? Array.Empty<Tag>()).ToArray();
            Environment = environment;
            Application = application;
            Replica = replica;
        }

        public string Environment { get; }

        public string Application { get; }

        public string Replica { get; }

        public Tag[] Tags { get; }

        public IReadOnlyDictionary<string, string> Properties => properties;

        public void SetProperty([NotNull] string key, [CanBeNull] string value)
        {
            properties[key ?? throw new ArgumentNullException(nameof(key))] = value;
        }
    }
}
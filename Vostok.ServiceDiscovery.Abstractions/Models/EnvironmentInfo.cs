﻿using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Vostok.ServiceDiscovery.Abstractions.Models
{
    public class EnvironmentInfo : IEnvironmentInfo
    {
        public EnvironmentInfo([NotNull] string environment, [CanBeNull] string parentEnvironment, [CanBeNull] IReadOnlyDictionary<string, string> properties)
        {
            if (string.IsNullOrWhiteSpace(environment))
                throw new ArgumentOutOfRangeException(nameof(environment), environment);
            Environment = environment;
            ParentEnvironment = parentEnvironment;
            Properties = new EnvironmentInfoProperties(properties);
        }

        public string Environment { get; }

        public string ParentEnvironment { get; }

        public IEnvironmentInfoProperties Properties { get; }
    }
}
using FluentAssertions;
using NUnit.Framework;
using Vostok.ServiceDiscovery.Abstractions.Models;

namespace Vostok.ServiceDiscovery.Abstractions.Tests
{
    [TestFixture]
    public class TagPropertyHelpers_Tests
    {
        [Test]
        [TestCase("Tags:replicaName1", true)]
        [TestCase("Tags:replicaName1:persistent", true)]
        [TestCase("Tags-replicaName1", false)]
        [TestCase("Tags=replicaName1:persistent", false)]
        [TestCase("TagsReplicaName1:persistent", false)]
        [TestCase("Tags:", true)]
        public void IsTagsPropertyKey_should_return_expected_result_on_given_key(string key, bool expected)
            => TagPropertyHelpers.IsTagsPropertyKey(key).Should().Be(expected);

        [Test]
        [TestCase("Tags:replicaName1", "replicaName1")]
        [TestCase("Tags:replicaName1:", "replicaName1")]
        [TestCase("Tags:replicaName1:persistent", "replicaName1")]
        [TestCase("Tags-replicaName1", null)]
        [TestCase("Tags=replicaName1:persistent", null)]
        [TestCase("TagsReplicaName1:persistent", null)]
        [TestCase("Tags:", "")]
        [TestCase("Tags::kind", "")]
        public void ExtractReplicaName_should_return_expected_result_on_given_property_name(string appPropertyName, string expected)
           => TagPropertyHelpers.ExtractReplicaName(appPropertyName).Should().Be(expected);

        [Test]
        [TestCase("replicaName", "kind", "Tags:replicaName:kind")]
        [TestCase("", "kind", "Tags::kind")]
        [TestCase("replicaName", "", "Tags:replicaName:")]
        [TestCase("replica", "kind:hmmm", "replica:kind:hmmm:")] // todo should think about ":" sign escaping
        public void FormatName_should_return_expected_result_on_given_replica_and_kind(string replicaName, string tagKind, string expected)
            => TagPropertyHelpers.FormatName(replicaName, tagKind).Should().Be(expected);
    }
}
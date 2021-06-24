using FluentAssertions;
using NUnit.Framework;
using Vostok.ServiceDiscovery.Abstractions.Models;

namespace Vostok.ServiceDiscovery.Abstractions.Tests
{
    [TestFixture]
    public class TagPropertyKey_Tests
    {
        [Test]
        [TestCase("Tags|replicaName1|", "replicaName1", "")]
        [TestCase("Tags|replicaName1|persistent", "replicaName1", "persistent")]
        [TestCase("Tags||kind", "", "kind")]
        [TestCase("Tags||", "", "")]
        [TestCase("Tags|http://replicaName:4242/|", "http://replicaName:4242/", "")]
        [TestCase("Tags|http://replicaName:4242/|persistent", "http://replicaName:4242/", "persistent")]
        public void ExtractReplicaName_should_return_true_on_valid_property_name(string appPropertyName, string expectedReplicaName, string expectedTagKind)
        {
            TagsPropertyKey.TryParse(appPropertyName, out var actual).Should().BeTrue();
            actual.Should().NotBeNull();
            actual.ReplicaName.Should().Be(expectedReplicaName);
            actual.TagKind.Should().Be(expectedTagKind);
        }

        [Test]
        [TestCase("Tags|replicaName1")]
        [TestCase("Tags-replicaName1")]
        [TestCase("Tags=replicaName1|persistent")]
        [TestCase("TagsReplicaName1|persistent")]
        [TestCase("Tags|")]
        [TestCase("Tags")]
        public void ExtractReplicaName_should_return_false_on_invalid_property_name(string appPropertyName)
        {
            TagsPropertyKey.TryParse(appPropertyName, out var actual).Should().BeFalse();
            actual.Should().BeNull();
        }

        [Test]
        [TestCase("replicaName", "kind", "Tags|replicaName|kind")]
        [TestCase("", "kind", "Tags||kind")]
        [TestCase("replicaName", "", "Tags|replicaName|")]
        [TestCase("replica:1234", "kind:hmmm", "Tags|replica:1234|kind:hmmm")]
        public void FormatName_should_return_expected_result_on_given_replica_and_kind(string replicaName, string tagKind, string expected)
            => new TagsPropertyKey(replicaName, tagKind).ToString().Should().Be(expected);
    }
}
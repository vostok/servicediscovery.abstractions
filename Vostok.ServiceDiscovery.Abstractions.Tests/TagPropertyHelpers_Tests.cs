using FluentAssertions;
using NUnit.Framework;
using Vostok.ServiceDiscovery.Abstractions.Models;

namespace Vostok.ServiceDiscovery.Abstractions.Tests
{
    [TestFixture]
    public class TagPropertyHelpers_Tests
    {
        [Test]
        [TestCase("Tags|replicaName1", true)]
        [TestCase("Tags|replicaName1|persistent", true)]
        [TestCase("Tags-replicaName1", false)]
        [TestCase("Tags=replicaName1:persistent", false)]
        [TestCase("TagsReplicaName1|persistent", false)]
        [TestCase("Tags|", true)]
        [TestCase("", false)]
        [TestCase(null, false)]
        [TestCase("Tags|http://replicaName:4242/", true)]
        [TestCase("Tags|http://replicaName:4242/|persistent", true)]
        public void IsTagsPropertyKey_should_return_expected_result_on_given_key(string key, bool expected)
            => TagPropertyHelpers.IsTagsPropertyKey(key).Should().Be(expected);
        
        [Test]
        [TestCase("Tags|replicaName1", "replicaName1", true)]
        [TestCase("Tags|replicaName1|persistent", "replicaName1", true)]
        [TestCase("Tags-replicaName1", "replicaName1", false)]
        [TestCase("Tags=replicaName1|persistent", "replicaName1", false)]
        [TestCase("TagsReplicaName1|persistent", "replicaName1", false)]
        [TestCase("Tags|", "replicaName1", false)]
        [TestCase("Tags|replicaName1", "replicaName1", true)]
        [TestCase("Tags|replicaName1|persistent", "replicaName2", false)]
        [TestCase(null, "replicaName2", false)]
        [TestCase("Tags|", "", true)]
        [TestCase("Tags|", null, true)]
        [TestCase("Tags||persistent", "", true)]
        [TestCase("Tags|replicaName1", "", false)]
        [TestCase("Tags|replicaName1:persistent", "", false)]
        [TestCase("Tags|http://replicaName:4242/", "http://replicaName:4242/", true)]
        [TestCase("Tags|http://replicaName:4242/|persistent", "http://replicaName:4242/", true)]
        public void IsReplicaTagsPropertyKey_should_return_expected_result_on_given_key(string key, string replicaName, bool expected)
            => TagPropertyHelpers.IsReplicaTagsPropertyKey(key, replicaName).Should().Be(expected);

        [Test]
        [TestCase("Tags|replicaName1", "replicaName1")]
        [TestCase("Tags|replicaName1|", "replicaName1")]
        [TestCase("Tags|replicaName1|persistent", "replicaName1")]
        [TestCase("Tags-replicaName1", null)]
        [TestCase("Tags=replicaName1|persistent", null)]
        [TestCase("TagsReplicaName1|persistent", null)]
        [TestCase("Tags|", "")]
        [TestCase("Tags||kind", "")]
        public void ExtractReplicaName_should_return_expected_result_on_given_property_name(string appPropertyName, string expected)
           => TagPropertyHelpers.ExtractReplicaName(appPropertyName).Should().Be(expected);

        [Test]
        [TestCase("replicaName", "kind", "Tags|replicaName|kind")]
        [TestCase("", "kind", "Tags||kind")]
        [TestCase("replicaName", "", "Tags|replicaName|")]
        [TestCase("replica:1234", "kind:hmmm", "Tags|replica:1234|kind:hmmm")]
        public void FormatName_should_return_expected_result_on_given_replica_and_kind(string replicaName, string tagKind, string expected)
            => TagPropertyHelpers.FormatName(replicaName, tagKind).Should().Be(expected);
    }
}
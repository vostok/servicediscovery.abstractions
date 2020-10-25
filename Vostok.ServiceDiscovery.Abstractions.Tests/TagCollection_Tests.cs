using FluentAssertions;
using NUnit.Framework;
using Vostok.ServiceDiscovery.Abstractions.Models;

namespace Vostok.ServiceDiscovery.Abstractions.Tests
{
    [TestFixture]
    public class TagCollection_Tests
    {
        [Test]
        public void TryParse_should_return_empty_tag_collection_on_empty_string()
        {
            TagCollection.TryParse("", out var actual).Should().BeTrue();
            actual.Should().BeEquivalentTo(new TagCollection());
        }

        [Test]
        public void TryParse_should_right_tag_collection_on_given_string_with_valid_nodes()
        {
            const string initial = "tag1═value1|═tag5|tag═val=ue|tag=val═ue|tag2═";
            var expected = new TagCollection
            {
                {"tag1", "value1"},
                {"", "tag5"},
                {"tag", "val=ue"},
                {"tag=val", "ue"},
                {"tag2", ""}
            };
            TagCollection.TryParse(initial, out var actual).Should().BeTrue();
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        [TestCase("tag7?value777")]
        [TestCase("tag")]
        [TestCase("tag|tag═value")]
        [TestCase("tag=value|tag")]
        [TestCase("tag4:true")]
        [TestCase("tag1═true|")]
        [TestCase("|tag1═true")]
        [TestCase("tag1═v1||tag2═v1")]
        [TestCase("tag1═v1|tag1═v2")]
        public void TryParse_should_return_null_tag_collection_and_false_on_bad_string(string badString)
        {
            TagCollection.TryParse(badString, out var actual).Should().BeFalse();
            actual.Should().BeNull();
        }

        [Test]
        public void ToString_should_return_empty_string_on_empty_collection()
        {
            new TagCollection().ToString().Should().BeEmpty();
        }

        [Test]
        public void ToString_should_return_right_string_on_given_collection()
        {
            var collection = new TagCollection
            {
                {"tag1", "value1"},
                {"tag2", "123"},
                "tag3"
            };
            collection.ToString().Should().Be("tag1═value1|tag2═123|tag3═true");
        }

        [Test]
        public void Equals_should_return_true_with_tag_collections_with_other_initialization_order()
        {
            var collection1 = new TagCollection {"tag1", {"tag2", "value1"}, "tag3"};
            var collection2 = new TagCollection {{"tag2", "value1"}, "tag1", "tag3"};
            collection1.Equals(collection2).Should().BeTrue();
        }

        [Test]
        public void Equals_should_return_true_on_tag_collections_with_same_key_in_different_cases()
        {
            var collection1 = new TagCollection {"tag1", {"tag2", "value1"}, "tag3"};
            var collection2 = new TagCollection {"TAG1", {"tag2", "value1"}, "tag3"};
            collection1.Equals(collection2).Should().BeTrue();
        }

        [Test]
        public void Equals_should_return_false_on_tag_collections_with_same_values_in_different_cases()
        {
            var collection1 = new TagCollection {"tag1", {"tag2", "value1"}, "tag3"};
            var collection2 = new TagCollection {"tag1", {"tag2", "VALUE1"}, "tag3"};
            collection1.Equals(collection2).Should().BeFalse();
        }

        [Test]
        public void Equals_should_return_false_with_other_members_count()
        {
            var collection1 = new TagCollection {"tag1", {"tag2", "value1"}, "tag3", "tag4"};
            var collection2 = new TagCollection {"TAG1", {"tag2", "value1"}, "tag3"};
            collection1.Equals(collection2).Should().BeFalse();
        }
    }
}
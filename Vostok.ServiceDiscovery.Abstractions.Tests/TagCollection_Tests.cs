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
        public void ToString_should_return_empty_string_then_tag_collection_is_empty()
        {
            new TagCollection().ToString().Should().Be("");
        }

        [Test] // todo should think about edge cases
        public void TryParse_should_return_empty_tag_collection_on_bad_string()
        {
            const string initial = "tag1|=tag2|tag3=";
            var expected = new TagCollection
            {
                {"", "tag2"},
                {"tag3", ""},
            };
            TagCollection.TryParse(initial, out var actual).Should().BeTrue();
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void TryParse_should_return_right_tag_collection_on_initial_string()
        {
            const string initial = "tag1=value1|tag2=123|tag3=true";
            var expected = new TagCollection
            {
                {"tag1", "value1"},
                {"tag2", "123"},
                "tag3"
            };
            TagCollection.TryParse(initial, out var actual).Should().BeTrue();
            actual.Should().BeEquivalentTo(expected);
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
            const string expected = "tag1=value1|tag2=123|tag3=true";
            collection.ToString().Should().Be(expected);
        }

        [Test]
        public void TryParse_should_return_right_tag_collection_on_given_string_with_broken_nodes()
        {
            const string initial = "tag1=value1|tag7?value777|tag=value=tag|tag2=123|tag5|tag3=true|tag4:123";
            TagCollection.TryParse(initial, out var actual).Should().BeTrue();
            var expected = new TagCollection
            {
                {"tag1", "value1"},
                {"tag", "value=tag"},
                {"tag2", "123"},
                "tag3",
            };
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Equals_should_return_true_with_tag_collections_with_other_initialization_order()
        {
            var collection1 = new TagCollection { "tag1", {"tag2", "value1"}, "tag3" };
            var collection2 = new TagCollection {{"tag2", "value1"}, "tag1", "tag3" };
            collection1.Equals(collection2).Should().BeTrue();
        }

        [Test]
        public void Equals_should_return_false_on_tag_collections_with_same_key_values_with_other_case()
        {
            var collection1 = new TagCollection { "tag1", {"tag2", "value1"}, "tag3" };
            var collection2 = new TagCollection {"TAG1", {"tag2", "VALUE1"}, "tag3" };
            collection1.Equals(collection2).Should().BeFalse();
        }

        [Test]
        public void Equals_should_return_false_with_other_members_count()
        {
            var collection1 = new TagCollection { "tag1", {"tag2", "value1"}, "tag3", "tag4" };
            var collection2 = new TagCollection {"TAG1", {"tag2", "value1"}, "tag3" };
            collection1.Equals(collection2).Should().BeFalse();
        }
    }
}
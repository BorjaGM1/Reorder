using Reorder;
using FluentAssertions;

namespace Tests
{
    public class GapTests
    {

        OrderManager<TestObject> manager = new OrderManager<TestObject>();

        [Fact]
        public void TestNoGaps()
        {
            List<TestObject> init = new List<TestObject>
            {
                new TestObject("id1", 1),
                new TestObject("id2", 3),
                new TestObject("id3", 5),
                new TestObject("id4", 7),
                new TestObject("id5", 9)
            };

            List<TestObject> expected = new List<TestObject>
            {
                new TestObject("id1", 1),
                new TestObject("id2", 2),
                new TestObject("id3", 3),
                new TestObject("id4", 4),
                new TestObject("id5", 5)
            };

            var actual = manager.RemoveOrderGaps(init);

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void TestWithGaps()
        {
            List<TestObject> init = new List<TestObject>
            {
                new TestObject("id1", 1),
                new TestObject("id2", 3),
                new TestObject("id3", 5),
                new TestObject("id4", 7),
                new TestObject("id5", 9)
            };

            List<TestObject> expected = new List<TestObject>
            {
                new TestObject("id1", 1),
                new TestObject("id2", 2),
                new TestObject("id3", 3),
                new TestObject("id4", 4),
                new TestObject("id5", 6)
            };

            var actual = manager.RemoveOrderGaps(init);

            actual.Should().NotBeEquivalentTo(expected);
        }
    }
}
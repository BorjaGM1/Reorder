

namespace Tests
{
    public class ReorderTests
    {

        OrderManager<TestObject> manager = new OrderManager<TestObject>();

        Func<TestObject, string> getId = x => x.Id;
        Func<TestObject, uint> getOrder = x => x.Order;

        List<TestObject> init = new List<TestObject>
            {
                new TestObject("id1", 1),
                new TestObject("id2", 3),
                new TestObject("id3", 5),
                new TestObject("id4", 7),
                new TestObject("id5", 9)
            };

        [Fact]
        public void TestMoveLower()
        {
            List<TestObject> expected = new List<TestObject>
            {
                new TestObject("id1", 2),
                new TestObject("id2", 3),
                new TestObject("id3", 1),
                new TestObject("id4", 4),
                new TestObject("id5", 5)
            };

            var actual = manager.MoveObject(
                init,
                getId,
                getOrder,
                (obj, order) => obj.Order = order,
                "id3",
                1
                );

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void TestMoveLowerFails()
        {
            List<TestObject> expected = new List<TestObject>
            {
                new TestObject("id1", 6),
                new TestObject("id2", 3),
                new TestObject("id3", 1),
                new TestObject("id4", 4),
                new TestObject("id5", 5)
            };

            var actual = manager.MoveObject(
                init,
                getId,
                getOrder,
                (obj, order) => obj.Order = order,
                "id3",
                1
                );

            actual.Should().NotBeEquivalentTo(expected);
        }

        [Fact]
        public void MoveHigherWithinGaps()
        {
            List<TestObject> expected = new List<TestObject>
            {
                new TestObject("id1", 1),
                new TestObject("id2", 2),
                new TestObject("id3", 5),
                new TestObject("id4", 3),
                new TestObject("id5", 4)
            };
            //This test shouldn't pass. id3 should end as 4th, not 5th.
            var actual = manager.MoveObject(
                init,
                getId,
                getOrder,
                (obj, order) => obj.Order = order,
                "id3",
                8
                );

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void TestMoveHigerFails()
        {
            List<TestObject> expected = new List<TestObject>
            {
                new TestObject("id1", 1),
                new TestObject("id2", 2),
                new TestObject("id3", 5),
                new TestObject("id4", 3),
                new TestObject("id5", 4)
            };

            var actual = manager.MoveObject(
                init,
                getId,
                getOrder,
                (obj, order) => obj.Order = order,
                "id3",
                5
                );

            actual.Should().NotBeEquivalentTo(expected);
        }
    }
}
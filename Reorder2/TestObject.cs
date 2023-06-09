namespace Reorder
{
    public class TestObject : IOrderable
    {
        public string Id { get; }
        public uint Order { get; set; }

        public TestObject(string id, uint order)
        {
            Id = id;
            Order = order;
        }
    }
}

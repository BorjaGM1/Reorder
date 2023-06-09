using Reorder;

public class Program
{
    public static void Main()
    {
        int objectCount = 10;
        OrderManager<TestObject> orderManager = new OrderManager<TestObject>();
        ManipulateTestObjects(orderManager, objectCount);

        Console.ReadLine();
    }

    public static void ManipulateTestObjects(OrderManager<TestObject> orderManager, int objectCount)
    {
        //List<TestObject> initialList = GenerateRandomObjects(objectCount);

        List<TestObject> initialList = new List<TestObject>
            {
                new TestObject("id1", 1),
                new TestObject("id2", 3),
                new TestObject("id3", 5),
                new TestObject("id4", 7),
                new TestObject("id5", 9)
            };

        Console.WriteLine("Initial List:");
        PrintObjects(initialList);

        List<TestObject> movedList = orderManager.MoveObject(
            initialList,
            obj => obj.Id,
            obj => obj.Order,
            (obj, order) => obj.Order = order,
            "id4",
            2);
        Console.WriteLine("\nList after moving 'id4' to position 2:");
        PrintObjects(movedList);

        // Rest of the code...

    }

    private static List<TestObject> GenerateRandomObjects(int count)
    {
        List<TestObject> objects = new List<TestObject>();
        Random random = new Random();

        for (int i = 1; i <= count; i++)
        {
            string id = "id" + i;
            uint order = (uint)i;
            objects.Add(new TestObject(id, order));
        }

        return objects;
    }

    private static void PrintObjects(IEnumerable<TestObject> objects)
    {
        foreach (TestObject obj in objects)
        {
            Console.WriteLine($"Id: {obj.Id}, Order: {obj.Order}");
        }
    }
}





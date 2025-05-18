using InventarioMed.Shared.Data.BD;

internal class Program
{
    private static DAL<Order> orderDAL = new();
    private static DAL<Item> itemDAL = new();
    private static DAL<Tag> tagDAL = new();

    private static void Main()
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Order Management System:");
            Console.WriteLine("1 - Create new order");
            Console.WriteLine("2 - Add item to order");
            Console.WriteLine("3 - Add tag to order");
            Console.WriteLine("4 - List all orders");
            Console.WriteLine("5 - List tags of order");
            Console.WriteLine("-1 - Exit");
            Console.Write("Select option: ");

            int option = int.Parse(Console.ReadLine()!);

            switch (option)
            {
                case 1: CreateOrder(); break;
                case 2: AddItem(); break;
                case 3: AddTag(); break;
                case 4: ShowOrders(); break;
                case 5: ShowTags(); break;
                case -1: exit = true; break;
                default: Console.WriteLine("Invalid option."); break;
            }
        }
    }

    private static void CreateOrder()
    {
        Console.Write("Enter order description: ");
        string? desc = Console.ReadLine();
        orderDAL.Create(new Order { Description = desc });
        Console.WriteLine("Order created.");
    }

    private static void AddItem()
    {
        Console.Write("Enter order ID: ");
        int orderId = int.Parse(Console.ReadLine()!);
        Console.Write("Enter item name: ");
        string? itemName = Console.ReadLine();

        itemDAL.Create(new Item { Name = itemName, OrderId = orderId });
        Console.WriteLine("Item added.");
    }

    private static void AddTag()
    {
        Console.Write("Enter order ID: ");
        int orderId = int.Parse(Console.ReadLine()!);
        Console.Write("Enter tag label: ");
        string? label = Console.ReadLine();

        var tag = new Tag { Label = label };
        tagDAL.Create(tag);

        var order = orderDAL.ReadBy(o => o.Id == orderId);
        if (order != null)
        {
            order.Tags.Add(tag);
            orderDAL.Update(order);
            Console.WriteLine("Tag added.");
        }
        else
        {
            Console.WriteLine("Order not found.");
        }
    }

    private static void ShowOrders()
    {
        var orders = orderDAL.Read();
        foreach (var order in orders)
        {
            Console.WriteLine($"Order {order.Id}: {order.Description}");
            foreach (var item in order.Items)
            {
                Console.WriteLine($"  - Item: {item.Name}");
            }
        }
    }

    private static void ShowTags()
    {
        Console.Write("Enter order ID: ");
        int orderId = int.Parse(Console.ReadLine()!);

        var order = orderDAL.ReadBy(o => o.Id == orderId);
        if (order != null)
        {
            foreach (var tag in order.Tags)
            {
                Console.WriteLine($"- Tag: {tag.Label}");
            }
        }
        else
        {
            Console.WriteLine("Order not found.");
        }
    }
}

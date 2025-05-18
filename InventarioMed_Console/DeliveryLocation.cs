namespace EncomendasConsole;

public class DeliveryLocation
{
    public string Address { get; set; }
    public List<Order> Orders { get; set; } = new();

    public DeliveryLocation(string address)
    {
        Address = address;
    }

    public void ShowOrders()
    {
        Console.WriteLine($"Encomendas com entrega em {Address}:");
        foreach (var order in Orders)
        {
            Console.WriteLine($"- {order.Code} ({order.Customer})");
        }
    }

    public override bool Equals(object obj)
    {
        return obj is DeliveryLocation location && Address == location.Address;
    }

    public override int GetHashCode()
    {
        return Address.GetHashCode();
    }
}

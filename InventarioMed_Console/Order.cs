namespace EncomendasConsole;

public class Order
{
    public string Code { get; set; }
    public string Customer { get; set; }

    public List<OrderItem> Items { get; set; } = new();
    public List<DeliveryLocation> Locations { get; set; } = new();

    public Order(string code, string customer)
    {
        Code = code;
        Customer = customer;
    }

    public void AddItem(OrderItem item)
    {
        Items.Add(item);
    }

    public void AddLocation(DeliveryLocation location)
    {
        if (!Locations.Contains(location))
        {
            Locations.Add(location);
            location.Orders.Add(this); // N:N
        }
    }

    public void ShowItems()
    {
        Console.WriteLine($"Itens da encomenda {Code} ({Customer}):");
        foreach (var item in Items)
        {
            Console.WriteLine($"- {item.Description}, Quantidade: {item.Quantity}");
        }
    }

    public void ShowLocations()
    {
        Console.WriteLine($"Locais de entrega da encomenda {Code} ({Customer}):");
        foreach (var loc in Locations)
        {
            Console.WriteLine($"- {loc.Address}");
        }
    }

    public override string ToString()
    {
        return $"Código: {Code}, Cliente: {Customer}, Itens: {Items.Count}, Locais: {Locations.Count}";
    }
}

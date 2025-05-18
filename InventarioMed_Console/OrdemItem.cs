namespace EncomendasConsole;

public class OrderItem
{
    public string Description { get; set; }
    public int Quantity { get; set; }

    public OrderItem(string description, int quantity)
    {
        Description = description;
        Quantity = quantity;
    }
}

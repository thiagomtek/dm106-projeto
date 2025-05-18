namespace InventarioMed.Shared.Data.BD
{
    public class Order
    {
        public int Id { get; set; }
        public string? Description { get; set; }

        public virtual List<Item> Items { get; set; } = new();
        public virtual List<Tag> Tags { get; set; } = new();
    }
}

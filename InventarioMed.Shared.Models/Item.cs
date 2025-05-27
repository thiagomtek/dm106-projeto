namespace InventarioMed.Shared.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;

        public int OrderId { get; set; }
        public virtual Order Order { get; set; } = null!;
    }
}

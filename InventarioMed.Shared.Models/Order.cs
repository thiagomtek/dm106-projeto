namespace InventarioMed.Shared.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string ClientName { get; set; } = string.Empty;

        public virtual ICollection<Item> Items { get; set; } = new List<Item>();
        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}

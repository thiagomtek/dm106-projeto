namespace InventarioMed.Shared.Data.BD
{
    public class Tag
    {
        public int Id { get; set; }
        public string? Label { get; set; }

        public virtual List<Order> Orders { get; set; } = new();
    }
}

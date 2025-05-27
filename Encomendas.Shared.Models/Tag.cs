namespace Encomendas.Shared.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Label { get; set; } = string.Empty;

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}

namespace InventarioMed_API.DTOs
{
    public class OrderDTO
    {
        public string ClientName { get; set; } = string.Empty;
        public List<string> ProductNames { get; set; } = new();
        public List<string> Tags { get; set; } = new();
    }
}

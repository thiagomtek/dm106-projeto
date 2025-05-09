namespace EncomendasApi.Models;

public class Encomenda
{
    public int Id { get; set; }
    public string Destinatario { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;
    public string Status { get; set; } = "Pendente";
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
}

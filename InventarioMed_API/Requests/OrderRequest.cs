using Microsoft.AspNetCore.Http.HttpResults;

namespace InventarioMed_API.Requests
{
    public record OrderRequest(string ClientName, ICollection<TagRequest>? Tags = null);
}

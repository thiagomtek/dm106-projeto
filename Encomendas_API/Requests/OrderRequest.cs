using Microsoft.AspNetCore.Http.HttpResults;

namespace Encomendas_API.Requests
{
    public record OrderRequest(string ClientName, ICollection<TagRequest>? Tags = null);
}

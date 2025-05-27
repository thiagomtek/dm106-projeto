using Microsoft.AspNetCore.Http.HttpResults;

namespace Encomendas_API.Requests
{
    public record OrderEditRequest(int Id, string ClientName, ICollection<TagRequest>? Tags);
}

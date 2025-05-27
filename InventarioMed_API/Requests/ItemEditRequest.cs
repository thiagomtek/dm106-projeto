using Microsoft.AspNetCore.Http.HttpResults;

namespace InventarioMed_API.Requests
{
    public record OrderEditRequest(int Id, string ClientName, ICollection<TagRequest>? Tags);
}

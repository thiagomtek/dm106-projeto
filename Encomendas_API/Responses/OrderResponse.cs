using System;
using System.Collections.Generic;

namespace Encomendas_API.Responses
{
    public record OrderResponse(
        int id,
        string orderNumber,
        DateTime date,
        ICollection<int> itemIds = null,
        ICollection<int> tagIds = null);
}

using Microsoft.AspNetCore.Mvc;
using InventarioMed.Shared.Data.BD;
using InventarioMed.Shared.Entities;
using InventarioMed_API.DTOs;

namespace InventarioMed_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly DAL<Order> orderDAL;
        private readonly DAL<Tag> tagDAL;

        public OrderController()
        {
            orderDAL = new();
            tagDAL = new();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetOrders()
        {
            return Ok(orderDAL.Read());
        }

        [HttpPost]
        public ActionResult CreateOrder([FromBody] OrderDTO dto)
        {
            var newOrder = new Order
            {
                ClientName = dto.ClientName,
                Items = dto.ProductNames.Select(p => new Item { ProductName = p }).ToList(),
                Tags = dto.Tags.Select(t =>
                {
                    var existing = tagDAL.ReadBy(tag => tag.Label == t);
                    return existing ?? new Tag { Label = t };
                }).ToList()
            };

            orderDAL.Create(newOrder);
            return Ok(newOrder);
        }
    }
}

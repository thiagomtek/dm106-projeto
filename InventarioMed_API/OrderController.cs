using InventarioMed.Shared.Data.BD;
using Microsoft.AspNetCore.Mvc;

namespace InventarioMed_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly DAL<Order> orderDAL = new();

        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return orderDAL.Read();
        }
    }
}

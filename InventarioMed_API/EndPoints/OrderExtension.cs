using InventarioMed.Shared.Data.BD;
using InventarioMed.Shared.Entities;
using InventarioMed_API.Requests;
using InventarioMed_API.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InventarioMed_API.EndPoints
{
    public static class OrderExtension
    {
        public static void AddEndPointsOrder(this WebApplication app)
        {
            var groupBuilder = app.MapGroup("order")
                .RequireAuthorization()
                .WithTags("Orders");

            groupBuilder.MapGet("", ([FromServices] DAL<Order> dal) =>
            {
                var orderList = dal.Read();
                if (orderList == null) return Results.NotFound();
                var orderResponseList = EntityListToResponseList(orderList);
                return Results.Ok(orderResponseList);
            });

            groupBuilder.MapPost("", ([FromServices] DAL<Order> dal, [FromServices] DAL<Tag> tagDal, [FromBody] OrderRequest orderRequest) =>
            {
                var newOrder = new Order
                {
                    ClientName = orderRequest.ClientName,
                    Tags = orderRequest.Tags != null ? ConvertTagRequests(orderRequest.Tags, tagDal) : new List<Tag>()
                };

                dal.Create(newOrder);
                return Results.Created();
            });

            groupBuilder.MapDelete("/{id}", ([FromServices] DAL<Order> dal, int id) =>
            {
                var order = dal.ReadBy(o => o.Id == id);
                if (order is null) return Results.NotFound();
                dal.Delete(order);
                return Results.NoContent();
            });
        }

        private static List<Tag> ConvertTagRequests(ICollection<TagRequest> tagRequests, DAL<Tag> tagDal)
        {
            var tags = new List<Tag>();

            foreach (var request in tagRequests)
            {
                var tag = new Tag { Name = request.Name };
                var existingTag = tagDal.ReadBy(t => t.Name.ToUpper() == tag.Name.ToUpper());

                tags.Add(existingTag ?? tag);
            }

            return tags;
        }

        private static List<OrderResponse> EntityListToResponseList(IEnumerable<Order> orders)
        {
            return orders.Select(o => EntityToResponse(o)).ToList();
        }

        private static OrderResponse EntityToResponse(Order order)
        {
            return new OrderResponse
            {
                Id = order.Id,
                ClientName = order.ClientName,
                Tags = order.Tags?.Select(t => new TagResponse(t.Id, t.Name)).ToList() ?? new List<TagResponse>()
            };
        }
    }
}

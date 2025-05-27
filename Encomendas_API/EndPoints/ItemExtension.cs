using Encomendas.Shared.Data.BD;
using Encomendas.Shared.Entities;
using Encomendas_API.Requests;
using Encomendas_API.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Encomendas_API.EndPoints
{
    public static class ItemExtension
    {
        public static void AddEndPointsItem(this WebApplication app)
        {
            var groupBuilder = app.MapGroup("item")
                .RequireAuthorization()
                .WithTags("Items");

            groupBuilder.MapGet("", ([FromServices] DAL<Item> dal) =>
            {
                var itemList = dal.Read();
                if (itemList == null) return Results.NotFound();
                var itemResponseList = EntityListToResponseList(itemList);
                return Results.Ok(itemResponseList);
            });

            groupBuilder.MapGet("/{id}", (int id, [FromServices] DAL<Item> dal) =>
            {
                var item = dal.ReadBy(i => i.Id == id);
                if (item is null) return Results.NotFound();
                return Results.Ok(EntityToResponse(item));
            });

            groupBuilder.MapPost("", ([FromServices] DAL<Item> dal, [FromServices] DAL<Order> orderDal, [FromBody] ItemRequest itemRequest) =>
            {
                var order = orderDal.ReadBy(o => o.Id == itemRequest.OrderId);
                if (order is null) return Results.NotFound("Order not found");

                var newItem = new Item
                {
                    Name = itemRequest.Name,
                    Quantity = itemRequest.Quantity,
                    Order = order
                };

                dal.Create(newItem);
                return Results.Created();
            });

            groupBuilder.MapDelete("/{id}", ([FromServices] DAL<Item> dal, int id) =>
            {
                var item = dal.ReadBy(i => i.Id == id);
                if (item is null) return Results.NotFound();
                dal.Delete(item);
                return Results.NoContent();
            });

            groupBuilder.MapPut("", ([FromServices] DAL<Item> dal, [FromServices] DAL<Order> orderDal, [FromBody] ItemEditRequest itemRequest) =>
            {
                var itemToEdit = dal.ReadBy(i => i.Id == itemRequest.Id);
                if (itemToEdit is null) return Results.NotFound();

                itemToEdit.Name = itemRequest.Name;
                itemToEdit.Quantity = itemRequest.Quantity;

                if (itemRequest.OrderId != itemToEdit.Order?.Id)
                {
                    var newOrder = orderDal.ReadBy(o => o.Id == itemRequest.OrderId);
                    if (newOrder is null) return Results.NotFound("New Order not found");
                    itemToEdit.Order = newOrder;
                }

                dal.Update(itemToEdit);
                return Results.Created();
            });
        }

        private static ICollection<ItemResponse> EntityListToResponseList(IEnumerable<Item> itemList)
        {
            return itemList.Select(i => EntityToResponse(i)).ToList();
        }

        private static ItemResponse EntityToResponse(Item item)
        {
            return new ItemResponse(
                item.Id,
                item.Name,
                item.Quantity,
                item.Order?.Id ?? 0,
                item.Order?.ClientName ?? "No client"
            );
        }
    }
}

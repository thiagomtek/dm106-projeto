using Encomendas.Shared.Data.BD;
using Encomendas.Shared.Entities;
using Encomendas.Shared.Utils;
using Encomendas_API.Requests;
using Encomendas_API.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Encomendas_API.EndPoints
{
    public static class TagExtension
    {
        public static void AddEndPointsTag(this WebApplication app)
        {
            var groupBuilder = app.MapGroup("tag")
                .RequireAuthorization()
                .WithTags("Tags");

            groupBuilder.MapGet("", ([FromServices] DAL<Tag> dal) =>
            {
                var tagList = dal.Read();
                var tagResponseList = EntityListToResponseList(tagList);
                return Results.Ok(tagResponseList);
            });

            groupBuilder.MapGet("/{id}", (int id, [FromServices] DAL<Tag> dal) =>
            {
                var tag = dal.ReadBy(t => t.Id == id);
                if (tag is null) return Results.NotFound();
                return Results.Ok(EntityToResponse(tag));
            });

            groupBuilder.MapPost("", ([FromServices] DAL<Tag> dal, [FromBody] TagRequest tagRequest) =>
            {
                var newTag = new Tag { Name = tagRequest.Name };
                dal.Create(newTag);
                return Results.Ok();
            });

            groupBuilder.MapDelete("/{id}", ([FromServices] DAL<Tag> dal, int id) =>
            {
                var tag = dal.ReadBy(t => t.Id == id);
                if (tag is null) return Results.NotFound();
                dal.Delete(tag);
                return Results.NoContent();
            });

            groupBuilder.MapGet("/barcode/{id}", (int id, [FromServices] DAL<Tag> dal) =>
            {
                var tag = dal.ReadBy(t => t.Id == id);
                if (tag is null) return Results.NotFound("Tag not found.");

                var barcodeBase64 = BarcodeService.GenerateBarcodeBase64(tag.Name);
                return Results.Ok(new { tag.Id, tag.Name, Barcode = $"data:image/png;base64,{barcodeBase64}" });
            });

            groupBuilder.MapPut("", ([FromServices] DAL<Tag> dal, [FromBody] TagEditRequest tagRequest) =>
            {
                var tagToEdit = dal.ReadBy(t => t.Id == tagRequest.Id);
                if (tagToEdit is null) return Results.NotFound();
                tagToEdit.Name = tagRequest.Name;
                dal.Update(tagToEdit);
                return Results.Created();
            });
        }

        private static ICollection<TagResponse> EntityListToResponseList(IEnumerable<Tag> tagList)
        {
            return tagList.Select(t => EntityToResponse(t)).ToList();
        }

        private static TagResponse EntityToResponse(Tag tag)
        {
            return new TagResponse(
                tag.Id,
                tag.Name,
                tag.Orders?.Select(o => o.ClientName).ToList() ?? new List<string>()
            );
        }
    }
}

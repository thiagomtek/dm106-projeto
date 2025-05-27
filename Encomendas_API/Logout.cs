using Encomendas.Shared.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Encomendas_API
{
    public static class AuthExtension
    {
        public static void AddAuthEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("auth").WithTags("Authentication");

            group.MapPost("/logout", async (
                [FromServices] SignInManager<AccessUser> signInManager,
                HttpContext httpContext) =>
            {
                await signInManager.SignOutAsync();
                return Results.Ok(new { message = "Logout successful" });
            });
        }
    }

using InventarioMed.Shared.Data.BD;
using InventarioMed.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;
using InventarioMed.Shared.Data.Models;

var builder = WebApplication.CreateBuilder(args);

// Configura��o do JSON para evitar ciclos de refer�ncia
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Configura��o do DbContext
builder.Services.AddDbContext<EncomendasMedContext>();

// Configura��o do Identity
builder.Services
    .AddIdentityApiEndpoints<AccessUser>()
    .AddEntityFrameworkStores<EncomendasMedContext>();

builder.Services.AddAuthorization();

// Inje��o de depend�ncia para a DAL gen�rica com as novas entidades
builder.Services.AddTransient<DAL<Item>>();
builder.Services.AddTransient<DAL<Order>>();
builder.Services.AddTransient<DAL<Tag>>();

// Configura��o do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseAuthorization();

// Registro dos endpoints espec�ficos para as entidades
app.AddEndPointsItem();
app.AddEndPointsOrder();
app.AddEndPointsTag();

// Endpoints de autentica��o/autoriza��o
app.MapGroup("auth").MapIdentityApi<AccessUser>().WithTags("Authorization");

app.MapPost("auth/logout", async (HttpContext httpContext) => {
    var signInManager = httpContext.RequestServices.GetRequiredService<SignInManager<AccessUser>>();
    await signInManager.SignOutAsync();
    return Results.Ok();
})
.RequireAuthorization()
.WithTags("Authorization");

// Swagger UI
app.UseSwagger();
app.UseSwaggerUI();

app.Run();

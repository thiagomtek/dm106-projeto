using InventarioMed.Shared.Data.BD;
using InventarioMed.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;
using InventarioMed.Shared.Data.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuração do JSON para evitar ciclos de referência
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Configuração do DbContext
builder.Services.AddDbContext<EncomendasMedContext>();

// Configuração do Identity
builder.Services
    .AddIdentityApiEndpoints<AccessUser>()
    .AddEntityFrameworkStores<EncomendasMedContext>();

builder.Services.AddAuthorization();

// Injeção de dependência para a DAL genérica com as novas entidades
builder.Services.AddTransient<DAL<Item>>();
builder.Services.AddTransient<DAL<Order>>();
builder.Services.AddTransient<DAL<Tag>>();

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseAuthorization();

// Registro dos endpoints específicos para as entidades
app.AddEndPointsItem();
app.AddEndPointsOrder();
app.AddEndPointsTag();

// Endpoints de autenticação/autorização
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

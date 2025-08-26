using MarketWeight.Core;
using MarketWeight.Ado.Dapper;
using MarketWeight.Core.Persistencia;
using MySqlConnector;
using System.Data;
using Scalar.AspNetCore;
using MinimalAPI.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Configuración de la cadena de conexión
var connectionString = builder.Configuration.GetConnectionString("MySQL");

// Inyección de dependencias para la conexión y los repositorios
builder.Services.AddScoped<IDbConnection>(_ => new MySqlConnection(connectionString));
builder.Services.AddScoped<IRepoUsuario, RepoUsuario>();
builder.Services.AddScoped<IRepoMoneda, RepoMoneda>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "/openapi/{documentName}.json";
    });
    app.MapScalarApiReference();
}
// Endpoints para Usuario
app.MapPost("/usuarios", (IRepoUsuario repo, Usuario usuario) =>
{
    repo.Alta(usuario);
    return Results.Created($"/usuarios/{usuario.Email}", usuario);
});

// Endpoints para Moneda
app.MapGet("/monedas", (IRepoMoneda repo) => repo.Obtener());

app.MapGet("/monedas/{id:int}", (IRepoMoneda repo, uint id) =>
{
    var moneda = repo.Detalle(id);
    return moneda is not null ? Results.Ok(moneda) : Results.NotFound();
});

app.MapPost("/monedas", (IRepoMoneda repo, Moneda moneda) =>
{
    repo.Alta(moneda);
    return Results.Created($"/monedas/{moneda.Nombre}", moneda);
});

app.MapGet("/usuarios/{id:int}", async (IRepoUsuario repo, int id) =>
{
    var u = await repo.DetalleAsync((uint)id);
    if (u is null) return Results.NotFound();

    var dto = new UsuarioDTO
    {
        IdUsuario = u.idUsuario,
        Nombre = u.Nombre,
        Apellido = u.Apellido,
        Email = u.Email,
        Saldo = u.Saldo
    };

    return Results.Ok(dto);
});

app.MapGet("/usuarios", async (IRepoUsuario repo) =>
{
    var usuarios = await repo.ObtenerAsync();
    return usuarios.Select(u => new UsuarioDTO
    {
        IdUsuario = u.idUsuario,
        Nombre = u.Nombre,
        Apellido = u.Apellido,
        Email = u.Email,
        Saldo = u.Saldo
    });
});
app.Run();
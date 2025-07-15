using MarketWeight.Core;
using MarketWeight.Ado.Dapper;
using MarketWeight.Core.Persistencia;
using MySqlConnector;
using System.Data;
using Scalar.AspNetCore;


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
app.MapGet("/usuarios", (IRepoUsuario repo) => repo.Obtener());

app.MapGet("/usuarios/{id:int}", (IRepoUsuario repo, uint id) =>
{
    var usuario = repo.Detalle(id);
    return usuario is not null ? Results.Ok(usuario) : Results.NotFound();
});

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

app.Run();
using dafstore.API.Extensions;
using dafstore.API.Infrastructure;
using dafstore.Application.Shared;
using dafstore.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();

builder.Services.AddJwtAuthentication();
builder.Services.AddInfrastructure();
builder.Services.AddApplication();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Hello World!");

app.MapEndpoints();

app.Run();
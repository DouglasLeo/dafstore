using dafstore.Application.Shared;
using dafstore.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
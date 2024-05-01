using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add Services to the container
builder.Services
    .AddInfrastrucureServices(builder.Configuration)
    .AddAPIServices()
    .AddApplicationServices();



var app = builder.Build();

// Configure the HTTP request pipeline

app.UseAPIServices();

app.MapGet("/", () => "Hello World!");

app.Run();

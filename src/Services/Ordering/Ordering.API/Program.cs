using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add Services to the container
builder.Services
    .AddInfrastrucureServices(builder.Configuration)
    .AddAPIServices()
    .AddApplicationServices();


var app = builder.Build();

// Configure the HTTP request pipeline

app.UseAPIServices();

if(app.Environment.IsDevelopment())
{
  await app.InitialiseDatabaseAsync();
}

app.MapGet("/", () => "Hello World!");

app.Run();

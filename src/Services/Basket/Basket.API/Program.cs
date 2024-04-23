
using Basket.API.Data;
using BuildingBlocks.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);

// register container service
var assembly = typeof(Program).Assembly;
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddMarten(ops =>
{
    ops.Connection(builder.Configuration.GetConnectionString("Database")!);
    ops.Schema.For<ShoppingCart>().Identity(x => x.UserName);

}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

app.MapCarter();
app.UseExceptionHandler(opt => { });
app.Run();

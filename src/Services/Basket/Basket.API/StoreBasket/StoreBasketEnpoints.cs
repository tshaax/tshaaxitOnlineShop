
namespace Basket.API.StoreBasket
{
    public record StoreBasketRequest(ShoppingCart Cart);

    public record StoreBasketResponse(string UserName);
    public class StoreBasketEnpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
            {
                var command = request.Adapt<StoreBasketCommand>();

                var results = await sender.Send(command);

                var response = results.Adapt<StoreBasketResponse>();

                return Results.Created($"Created {response.UserName}", response);

            }).WithName("CreateBasket")
            .WithDescription("Create Basket")
            .WithSummary("Create Basket")
            .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound);
            
        }
    }
}

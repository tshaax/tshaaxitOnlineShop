
namespace Catalog.API.Products.GetProducts
{

    public record GetProductResponse(IEnumerable<Product> Products);

    public class GetProductsEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.Map("/products", async (ISender sender) =>
            {
                var results = await sender.Send(new GetProductsQuery());

                var response = results.Adapt<GetProductResponse>();

                return Results.Ok(response);
            }).WithName("GetProducts")
            .Produces<GetProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products")
            .WithDescription("Get Products");
        }
    }
}

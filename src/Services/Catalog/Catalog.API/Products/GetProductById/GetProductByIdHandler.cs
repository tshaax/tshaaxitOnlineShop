using Marten.Linq.QueryHandlers;

namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResults>;

    public record GetProductByIdResults(Product Product);


    internal class GetProductByIdQueryHandler (IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger): IQueryHandler<GetProductByIdQuery, GetProductByIdResults>
    {
        public async Task<GetProductByIdResults> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByIdQueryHandler.Handle called with {@Query}", query);

            var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

            if (product == null)
            {

                throw new ProductNotFoundException(query.Id);
            }

            return new GetProductByIdResults(product);
        }
    }
}

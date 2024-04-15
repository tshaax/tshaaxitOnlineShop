
namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid id) : ICommand<DeleteProductResult>;

    public record DeleteProductResult(bool IsSuccess);
    public class DeleteProductCommandHandler( IDocumentSession session, ILogger<DeleteProductCommandHandler> logger) 
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("DeleteProductCommandHandler handle {@command}", command);

            var product = await session.LoadAsync<Product>(command.id);

            if (product == null) {

                throw new ProductNotFoundException();
            }

            session.Delete(product);

           await session.SaveChangesAsync(cancellationToken);

            return new DeleteProductResult(true);
        }
    }
}

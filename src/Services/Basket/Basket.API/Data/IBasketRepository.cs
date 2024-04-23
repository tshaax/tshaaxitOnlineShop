namespace Basket.API.Data
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken);

        Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken cancellationToken);

        Task<bool> DeletsBasket(string UserName, CancellationToken cancellationToken);
    }
}

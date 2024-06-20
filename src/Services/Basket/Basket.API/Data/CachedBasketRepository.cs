

using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
    // cache-a-side pattern
    public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache) : IBasketRepository
    {
        public async Task<bool> DeletsBasket(string userName, CancellationToken cancellationToken)
        {
            await repository.DeletsBasket(userName, cancellationToken);

            await cache.RemoveAsync(userName);

            return true;
        }

        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken)
        {
            var basket = await cache.GetStringAsync(userName, cancellationToken);

            if (!string.IsNullOrEmpty(basket)) return JsonSerializer.Deserialize<ShoppingCart>(basket)!;

           var results = await repository.GetBasket(userName, cancellationToken);

            await cache.SetStringAsync(userName, JsonSerializer.Serialize(results));

            return results;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken cancellationToken)
        {
            var response = await repository.StoreBasket(cart, cancellationToken);

            await cache.SetStringAsync(response.UserName, JsonSerializer.Serialize(response));

            return response;
        }
    }
}

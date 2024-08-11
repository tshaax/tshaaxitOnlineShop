

using Shopping.Web.Models.Basket;

namespace Shopping.Web.Pages
{
    public class IndexModel(ICatalogService catalogService,IBasketService basketService, ILogger<IndexModel> _logger) : PageModel
    {
        public IEnumerable<ProductModel> ProductList { get; set; } = new List<ProductModel>();
        public async Task<IActionResult> OnGetAsync()
        {
            _logger.LogInformation("Index page visited");

            var result = await catalogService.GetProducts();

            ProductList = result.Products;

            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
        {
            _logger.LogInformation("Index page visited");

            var productResponse = await catalogService.GetProduct(productId);

            var basket = await basketService.LoadUserBasket();

            basket.Items.Add(new ShoppingCartItemModel
            {
                ProductId = productId,
                Price = productResponse.Product.Price,
                ProductName = productResponse.Product.Name,
                Quantity = 1,
                Color = "Black"
            });

            await basketService.StoreBasket(new StoreBasketRequest(basket));
            return RedirectToPage("Cart");
        }


    }
}

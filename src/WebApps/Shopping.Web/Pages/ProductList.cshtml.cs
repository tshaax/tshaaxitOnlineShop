using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.Web.Models.Basket;

namespace Shopping.Web.Pages
{
    public class ProductListModel(IBasketService basketService, ICatalogService catalogService, ILogger<ProductListModel> logger) : PageModel
    {
        public IEnumerable<string> CategoryList { get; set; } = [];
        public IEnumerable<ProductModel> ProductList { get; set; } = [];

        [BindProperty(SupportsGet = true)]
        public string SelectedCategory { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(string categoryName)
        {
            var response = await catalogService.GetProducts();

            CategoryList = response.Products.SelectMany(p => p.Category).Distinct().ToList();

            if(!string.IsNullOrWhiteSpace(categoryName))
            {
                ProductList = response.Products.Where(w => w.Category.Contains(categoryName)).ToList();
                SelectedCategory = categoryName;
            }
            else
            {
                ProductList = response.Products;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
        {
            logger.LogInformation("Add to cart button clicked");
            var productResponse = await catalogService.GetProduct(productId);

            var basket = await basketService.LoadUserBasket();

            basket.Items.Add(new ShoppingCartItemModel { 
                ProductId = productId,
                ProductName = productResponse.Product.Name,
                Color = "Black",
                Quantity = 1,
                Price = productResponse.Product.Price,

            });

            await basketService.StoreBasket(new StoreBasketRequest(basket));

            return RedirectToPage("Cart");

        }
    }
}

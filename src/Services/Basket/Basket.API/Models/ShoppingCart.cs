namespace Basket.API.Models
{
    public class ShoppingCart
    {
        public string UserName { get; set; } = default!;
        public List<ShoppingCartItem> Items { get; set; } = default!;
        public decimal TotalPrice => Items.Sum(s => s.Price * s.Quantity);


        public ShoppingCart(string userName) {
            UserName = userName;
        }

        //Required for Mapping
        public ShoppingCart() { }
    }
}

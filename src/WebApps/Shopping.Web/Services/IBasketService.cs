﻿using Shopping.Web.Models.Basket;

namespace Shopping.Web.Services
{
    public interface IBasketService
    {
        [Get("/basket-service/basket/{userName}")]
        Task<GetBasketResponse> GetBasket(string userName);

        [Post("/basket-service/basket")]
        Task<StoreBasketResponse> StoreBasket(StoreBasketRequest request);

        [Delete("/basket-service/basket/{userName}")]
        Task<DeleteBasketResponse> DeleteBasket(string userName);

        [Post("/basket-service/basket/checkout")]
        Task<CheckoutBasketRequest> CheckoutBasket(CheckoutBasketRequest request);

        public  async Task<ShoppingCartModel> LoadUserBasket()
        {
            var userName = "swn";
            ShoppingCartModel basket;

            try
            {
                var getBasketResponse = await GetBasket(userName);
                basket = getBasketResponse.Cart;

            }
            catch (ApiException apiException) when (apiException.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                basket = new ShoppingCartModel
                {
                    Items = [],
                    Username = userName,
                };
            }

            return basket;
        }
    }
}

using Basket.API.Data;
using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

    public record StoreBasketResult(string UserName);
    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null");
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required");

        }

    }

    public class StoreBasketCommandHandler(IBasketRepository repo, DiscountProtoService.DiscountProtoServiceClient discount) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            ShoppingCart cart = command.Cart;

            // TODO: store basket in database (use marten upsert)
            // TODO: update cache

            await DiscountPrice(cart, cancellationToken);

            var response = await repo.StoreBasket(cart, cancellationToken);

            return new StoreBasketResult(response.UserName);
        }

        public async Task DiscountPrice(ShoppingCart cart, CancellationToken cancellationToken)
        {
            foreach (var item in cart.Items)
            {
                var productDiscount = await discount.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);

                item.Price -= productDiscount.Amount;

            }
        }
    }
}

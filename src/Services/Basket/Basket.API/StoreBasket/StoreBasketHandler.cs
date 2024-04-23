

using Basket.API.Data;

namespace Basket.API.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart): ICommand<StoreBasketResult>;

    public record StoreBasketResult(string UserName);
    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator() 
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null");
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required");
        
        }

    }

    public class StoreBasketCommandHandler(IBasketRepository repo) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            ShoppingCart cart = command.Cart;

            // TODO: store basket in database (use marten upsert)
            // TODO: update cache

            var response = await repo.StoreBasket(cart, cancellationToken);

            return new StoreBasketResult(response.UserName);
        }
    }
}

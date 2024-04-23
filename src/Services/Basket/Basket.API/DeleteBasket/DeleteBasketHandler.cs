using FluentValidation;

namespace Basket.API.DeleteBasket
{

    public record DeleteBasketCommand(string UserName): ICommand<DeleteBasketResult>;

    public record DeleteBasketResult(bool IsSuccess);

    public class DeleteBasketValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketValidator() 
        { 
            RuleFor(x => x.UserName).NotNull().NotEmpty().WithMessage("UserName is required");
        }
    }
    public class DeleteBaskeCommandtHandler : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            // TODO DELETE FROM DB

            return new DeleteBasketResult(true);
        }
    }
}

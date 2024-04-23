using Basket.API.Data;
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
    public class DeleteBaskeCommandtHandler(IBasketRepository repo) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            // TODO DELETE FROM DB
            var res = await repo.DeletsBasket(command.UserName,cancellationToken);
            return new DeleteBasketResult(res);
        }
    }
}

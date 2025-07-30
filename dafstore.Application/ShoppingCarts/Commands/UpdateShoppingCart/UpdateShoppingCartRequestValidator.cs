using FluentValidation;

namespace dafstore.Application.ShoppingCarts.Commands.UpdateShoppingCart;

public class UpdateShoppingCartRequestValidator : AbstractValidator<UpdateShoppingCartCommand>
{
    public UpdateShoppingCartRequestValidator()
    {
        RuleFor(x => x.Items.Count).NotEmpty().WithMessage("Items is required.");
        RuleFor(x => x.Items.Count).GreaterThan(0).WithMessage("Must have at least one item.");
    }
}
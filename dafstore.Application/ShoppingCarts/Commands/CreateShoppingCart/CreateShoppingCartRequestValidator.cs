using FluentValidation;

namespace dafstore.Application.ShoppingCarts.Commands.CreateShoppingCart;

public class CreateShoppingCartRequestValidator : AbstractValidator<CreateShoppingCartCommand>
{
    public CreateShoppingCartRequestValidator()
    {
        RuleFor(x => x.Price).NotEmpty().WithMessage("Price is required.").GreaterThan(0)
            .WithMessage("Price must be greater than zero.");

        RuleFor(x => x.Quantity).NotEmpty().WithMessage("Quantity is required.").GreaterThan(0)
            .WithMessage("Quantity must be greater than zero.");
    }
}
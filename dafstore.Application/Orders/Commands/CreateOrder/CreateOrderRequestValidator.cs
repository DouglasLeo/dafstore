using FluentValidation;

namespace dafstore.Application.Orders.Commands.CreateOrder;

public class CreateOrderRequestValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderRequestValidator()
    {
        RuleFor(x => x.Items).NotEmpty().WithMessage("Must have at least one item.");
        
        RuleFor(x => x.PaymentMethod).IsInEnum().WithMessage("Payment Method needs to be an Enum.");
        
        RuleFor(x => x.DeliveryAddress).NotEmpty().WithMessage("Delivery Address is required.");
    }
}
using FluentValidation;

namespace dafstore.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderRequestValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderRequestValidator()
    {
        RuleFor(x => x.Status).IsInEnum().WithMessage("The Status must be an Enum.");
    }
}
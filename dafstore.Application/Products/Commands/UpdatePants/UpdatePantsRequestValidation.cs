using FluentValidation;

namespace dafstore.Application.Products.Commands.UpdatePants;

public class UpdatePantsRequestValidation : AbstractValidator<UpdatePantsCommand>
{
    public UpdatePantsRequestValidation()
    {
        RuleFor(x => x.Name).MinimumLength(3)
            .WithMessage("Name minimal length is 3.")
            .MaximumLength(100).WithMessage("Name maximum length is 100.");

        RuleFor(x => x.Description).MinimumLength(3)
            .WithMessage("Description minimal length is 3.")
            .MaximumLength(500).WithMessage("Description maximum length is 500.");

        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");

        RuleFor(x => x.Colors).ForEach(c =>
            c.MinimumLength(3).WithMessage("Color minimal length is 3."));

        RuleFor(x => x.Size).IsInEnum().WithMessage("Size must be even numbers.");
    }
}
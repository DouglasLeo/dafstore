using FluentValidation;

namespace dafstore.Application.Products.Commands.CreatePants;

public class CreatePantsRequestValidation : AbstractValidator<CreatePantsCommand>
{
    public CreatePantsRequestValidation()
    {
        RuleFor(x => x.Name).MinimumLength(3)
            .WithMessage("Name minimal length is 3.")
            .MaximumLength(100).WithMessage("Name maximum length is 100.");

        RuleFor(x => x.Description).MinimumLength(3)
            .WithMessage("Description minimal length is 3.")
            .MaximumLength(500).WithMessage("Description maximum length is 500.");
        
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");
        
        RuleFor(x => x.Quantity).NotEmpty().WithMessage("Quantity is required.")
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
        
        RuleFor(x => x.Colors).ForEach(c => 
            c.MinimumLength(3).WithMessage("Color minimal length is 3."));

        RuleFor(x => x.Size).IsInEnum().WithMessage("Size must be even numbers.");
        
        RuleFor(x => x.Categories).NotNull().WithMessage("The list cannot be null")
            .NotEmpty().WithMessage("The list must have at least one category")
            .ForEach(c => 
            c.SetValidator(new CategoryValidator()));
        
        RuleFor(x => x.TissueType).IsInEnum().WithMessage("TissueType must be even numbers.");
    }
}
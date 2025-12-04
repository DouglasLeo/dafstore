using dafstore.Application.Products.Commands.Shared;
using FluentValidation;

namespace dafstore.Application.Products.Commands;

public class CategoryValidator : AbstractValidator<CategoryDTO>
{
    public CategoryValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
    }
}
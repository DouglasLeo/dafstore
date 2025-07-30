using dafstore.Domain.Contexts.ProductContext.Entities;
using FluentValidation;

namespace dafstore.Application.Products.Commands;

public class CategoryValidator : AbstractValidator<Category>
{
    public CategoryValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
    }
}
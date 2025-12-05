using FluentValidation;

namespace dafstore.Application.Users.Commands.CreateRole;

public class CreateRoleRequestValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.").MinimumLength(3)
            .WithMessage("Name minimal length is 3.")
            .MaximumLength(50).WithMessage("Name maximum length is 50.");
    }
}
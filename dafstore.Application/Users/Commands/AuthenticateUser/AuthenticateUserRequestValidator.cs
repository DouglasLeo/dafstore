using FluentValidation;

namespace dafstore.Application.Users.Commands.AuthenticateUser;

public class AuthenticateUserRequestValidator : AbstractValidator<AuthenticateUserCommand>
{
    public AuthenticateUserRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email needs to be a valid email address.");
        
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("The password must be at least 8 characters.")
            .MaximumLength(100).WithMessage("The password must be a maximum 100 characters.")
            .Matches("[A-Z]").WithMessage("The password must be at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("The password must be at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("The password must be at least one number")
            .Matches("[^a-zA-Z0-9]").WithMessage("The password must be at least one special character.");
    }
}
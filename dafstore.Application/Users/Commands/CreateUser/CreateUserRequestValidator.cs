using FluentValidation;

namespace dafstore.Application.Users.Commands.CreateUser;

public class CreateUserRequestValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.").MinimumLength(3)
            .WithMessage("First name minimal length is 3.")
            .MaximumLength(100).WithMessage("First name maximum length is 100.");

        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.")
            .MinimumLength(3).WithMessage("Last name minimal length is 3.")
            .MaximumLength(100).WithMessage("Last name maximum length is 100.");

        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email needs to be a valid email address.");

        RuleFor(x => x.City).NotEmpty().WithMessage("City is required.");
        RuleFor(x => x.Country).NotEmpty().WithMessage("Country is required.");
        RuleFor(x => x.Street).NotEmpty().WithMessage("Country is required.");
        RuleFor(x => x.State).NotEmpty().WithMessage("Country is required.");
        RuleFor(x => x.Number).NotEmpty().WithMessage("Number is required.");
        RuleFor(x => x.ZipCode).NotEmpty().WithMessage("ZipCode is required.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("The password must be at least 8 characters.")
            .MaximumLength(100).WithMessage("The password must be a maximum 100 characters.")
            .Matches("[A-Z]").WithMessage("The password must be at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("The password must be at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("The password must be at least one number")
            .Matches("[^a-zA-Z0-9]").WithMessage("The password must be at least one special character.");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("The phone is required.")
            .Matches(@"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$")
            .WithMessage("The phone number must be in a valid format, such as (11) 91234-5678 or 11912345678.");
    }
}
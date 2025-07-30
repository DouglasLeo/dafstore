using FluentValidation;

namespace dafstore.Application.Users.Queries.GetUsers;

public class GetUserByEmailQueryValidator : AbstractValidator<GetUserByEmailQuery>
{
    public GetUserByEmailQueryValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Email needs to be a valid email address.");
    }
}
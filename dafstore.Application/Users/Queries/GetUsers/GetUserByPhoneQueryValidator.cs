using FluentValidation;

namespace dafstore.Application.Users.Queries.GetUsers;

public class GetUserByPhoneQueryValidator : AbstractValidator<GetUserByPhoneQuery>
{
    public GetUserByPhoneQueryValidator()
    {
        RuleFor(x => x.Phone)
            .NotEmpty()
            .WithMessage("Phone is required.")
            .Matches(@"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$")
            .WithMessage("The phone number must be in a valid format, such as (11) 91234-5678 or 11912345678.");
    }
}
using FluentValidation;

namespace dafstore.Application.Users.Queries.GetUsers;

public class GetAllUsersQueryValidator : AbstractValidator<GetAllUsersQuery>
{
    public GetAllUsersQueryValidator()
    {
        RuleFor(x => x.Skip)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Skip  must be greater or equal to 0.");

        RuleFor(x => x.Take)
            .InclusiveBetween(1, 100)
            .WithMessage("Take must be between 1 and 100.");
    }
}
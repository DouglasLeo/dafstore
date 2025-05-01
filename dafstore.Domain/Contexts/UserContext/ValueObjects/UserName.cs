using dafstore.Domain.Abstractions;

namespace dafstore.Domain.Contexts.UserContext.ValueObjects;

public record UserName(string FirstName, string LastName) : IValueObject
{
    public override string ToString() => $"{FirstName} {LastName}";
}
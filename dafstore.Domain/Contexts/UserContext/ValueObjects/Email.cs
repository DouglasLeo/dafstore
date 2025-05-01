using dafstore.Domain.Abstractions;

namespace dafstore.Domain.Contexts.UserContext.ValueObjects;

public record Email(string EmailAddress) : IValueObject
{
    public static implicit operator string(Email email) => email.ToString();

    public static implicit operator Email(string email) => new(email);

    public override string ToString() => EmailAddress;
}
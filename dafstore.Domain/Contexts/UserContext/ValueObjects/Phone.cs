using dafstore.Domain.Abstractions;

namespace dafstore.Domain.Contexts.UserContext.ValueObjects;

public record Phone(string PhoneNumber) : IValueObject
{
    public static implicit operator Phone(string phoneNumber) => new(phoneNumber);
    public override string ToString() => PhoneNumber;
}
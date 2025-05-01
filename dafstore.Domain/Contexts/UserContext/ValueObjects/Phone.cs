using dafstore.Domain.Abstractions;

namespace dafstore.Domain.Contexts.UserContext.ValueObjects;

public record Phone(string PhoneNumber) : IValueObject
{
    public override string ToString() => PhoneNumber;
}
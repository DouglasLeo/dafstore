using dafstore.Domain.Abstractions;

namespace dafstore.Domain.Contexts.UserContext.ValueObjects;

public record Address(
    string Street,
    string Number,
    string City,
    string State,
    string ZipCode,
    string Country,
    bool Active)
    : IValueObject
{
    public bool Active { get; private set; } = Active;

    public void SetActive(bool active) => Active = active;

    public override string ToString()
    {
        return $"{Street}, {Number} - {City} - {State} \n{ZipCode} - {Country}";
    }
}
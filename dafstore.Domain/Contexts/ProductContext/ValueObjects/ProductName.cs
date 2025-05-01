using dafstore.Domain.Abstractions;

namespace dafstore.Domain.Contexts.ProductContext.ValueObjects;

public record ProductName(string Name) : IValueObject
{
    public static implicit operator ProductName(string value) => new(value);
    public static implicit operator string(ProductName productName) => productName.ToString();
    public override string ToString() => Name;
}
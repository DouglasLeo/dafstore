namespace dafstore.Domain.Contexts.ProductContext.ValueObjects;

public record CategoryName(string Name)
{
    public static implicit operator CategoryName(string name) => new(name);
    public static implicit operator string(CategoryName categoryName) => categoryName.ToString();

    public override string ToString() => Name;
}
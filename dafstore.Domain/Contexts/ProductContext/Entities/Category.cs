using dafstore.Domain.Contexts.ProductContext.ValueObjects;
using dafstore.Domain.Shared.Entities;

namespace dafstore.Domain.Contexts.ProductContext.Entities;

public class Category : Entity
{
    private Category () { }
    
    public Category(CategoryName name, string? description = null)
    {
        Name = name;
        Description = description;
    }

    public CategoryName Name { get; private set; }
    public string? Description { get; private set; }

    public void UpdateName(CategoryName name) => Name = name;
    public void UpdateDescription(string description) => Description = description;
}
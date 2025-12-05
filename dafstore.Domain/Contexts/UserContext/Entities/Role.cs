using dafstore.Domain.Shared.Entities;

namespace dafstore.Domain.Contexts.UserContext.Entities;

public class Role : Entity
{
    private Role() { }
    
    public Role(string roleName)
    {
        Name = roleName;
    }

    public string Name { get; set; } = string.Empty;

    public List<User> Users { get; set; } = new();
}
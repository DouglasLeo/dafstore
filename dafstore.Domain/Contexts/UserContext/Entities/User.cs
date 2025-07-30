using dafstore.Domain.Contexts.OrderContext.Entities;
using dafstore.Domain.Contexts.ShoppingCartContext;
using dafstore.Domain.Contexts.UserContext.ValueObjects;
using dafstore.Domain.Shared.Entities;

namespace dafstore.Domain.Contexts.UserContext.Entities;

public class User : Entity
{
    private ICollection<Address> _adresses = [];
    private ICollection<Order> _orders = [];
    
    private User () { }
    
    public User(UserName userName, Email email, string password, Address address, Phone phone)
    {
        UserName = userName;
        Email = email;
        Password = password;
        AddAddress(address);
        Phone = phone;
        ShoppingCart = new ShoppingCart(Id);
    }

    public UserName UserName { get; private set; }
    public Email Email { get; private set; }
    public string Password { get; private set; }
    public IReadOnlyCollection<Address> Addresses => _adresses.ToArray();
    public Phone Phone { get; private set; }
    public ShoppingCart ShoppingCart { get; }
    public IReadOnlyCollection<Order> Orders => _orders.ToArray();
    
    public void AddAddress(Address address) => _adresses.Add(address);
    public void RemoveAddress(Address address) => _adresses.Remove(address);
    
    public void UpdateName(UserName userName) => UserName = userName;
    public void UpdateEmail(Email email) => Email = email;
    public void UpdatePassword(string password) => Password = password;
    public void UpdatePhone(Phone phoneNumber) => Phone = phoneNumber;
}
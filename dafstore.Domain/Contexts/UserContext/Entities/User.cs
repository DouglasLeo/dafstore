using dafstore.Domain.Contexts.UserContext.ValueObjects;
using dafstore.Domain.Shared.Entities;

namespace dafstore.Domain.Contexts.UserContext.Entities;

public class User : Entity
{
    private ICollection<Address> _adresses = [];
    
    public User(UserName userName, Email email, string password, Address addresses, Phone phoneNumber)
    {
        UserName = userName;
        Email = email;
        Password = password;
        AddAddress(addresses);
        PhoneNumber = phoneNumber;
    }

    public UserName UserName { get; private set; }
    public Email Email { get; private set; }
    public string Password { get; private set; }

    public IReadOnlyCollection<Address> AddressesList => _adresses.ToArray();
    public Phone PhoneNumber { get; private set; }
    
    public void AddAddress(Address address) => _adresses.Add(address);
    public void RemoveAddress(Address address) => _adresses.Remove(address);
    
    public void UpdateName(UserName userName) => UserName = userName;
    public void UpdateEmail(Email email) => Email = email;
    public void UpdatePassword(string password) => Password = password;
    public void UpdatePhone(Phone phoneNumber) => PhoneNumber = phoneNumber;
}
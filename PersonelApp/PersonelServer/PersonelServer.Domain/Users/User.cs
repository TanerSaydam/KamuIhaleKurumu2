using PersonelServer.Domain.Abstractions;

namespace PersonelServer.Domain.Users;
public sealed class User : Entity
{
    private User()
    {
        Name = new(string.Empty);
        Lastname = new(string.Empty);
        Email = new(string.Empty);
        PhoneNumber = new(string.Empty);
        Address = new(string.Empty, string.Empty, string.Empty, string.Empty);
    }
    public User(
        string name, 
        string lastname, 
        string email, 
        string phoneNumber, 
        string country, 
        string city, 
        string postalCode, 
        string fullAddress)
    {
        Name = new(name);
        Lastname = new(lastname);
        Email = new(email);
        PhoneNumber = new(phoneNumber);
        Address = new(country, city, postalCode, fullAddress);
    }
    public Name Name { get; private set; }
    public Lastname Lastname { get; private set; }
    public Email Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public Address Address { get; private set; }
}

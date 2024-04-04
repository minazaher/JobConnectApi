using Microsoft.AspNetCore.Identity;

namespace JobConnectApi.Models;

public class User
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; } 
    public string UserType { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public User()
    {
        UserId = Guid.NewGuid();
    }

    public User(string email, string password, string userType, string firstName, string lastName)
    {
        UserId = Guid.NewGuid();
        Email = email;
        Password = password;
        UserType = userType;
        FirstName = firstName;
        LastName = lastName;
    }
}

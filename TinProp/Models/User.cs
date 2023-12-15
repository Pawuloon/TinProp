using TinProp.Enums.Roles;

namespace TinProp.Models;

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public AppRole Role { get; set; }
    
    public User(string username, string password, string email, AppRole role)
    {
        Username = username;
        Password = password;
        Email = email;
        Role = role;
    }
}
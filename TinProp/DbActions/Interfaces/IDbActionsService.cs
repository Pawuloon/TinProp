using TinProp.Models;

namespace TinProp.DbActions.Interfaces;

public interface IDbActionsService
{
    public Task<int> AddUser(string username, string password, string email, int role);
    public Task<int> DeleteUser(string username);
    public Task<int> UpdateUser(string username, string password, string email, int role);
    public Task<User> GetUser(string username);
    
}
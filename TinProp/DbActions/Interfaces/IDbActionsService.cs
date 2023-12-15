using TinProp.Enums.Statuses;
using TinProp.Models;

namespace TinProp.DbActions.Interfaces;

public interface IDbActionsService
{
    public Task<Status> AddUser(string username, string password, string email, int role);
    public Task<Status> DeleteUser(string username);
    public Task<Status> UpdateUser(string username, string password, string email, int role);
    public Task<User> GetUser(string username);
    
}
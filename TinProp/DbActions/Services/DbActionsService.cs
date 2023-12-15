using TinProp.Constants.Interfaces;
using TinProp.DbActions.Interfaces;
using TinProp.Models;

namespace TinProp.DbActions.Services;


/// <summary>
///  This class is responsible for all database actions.
/// </summary>
public class DbActionsService : IDbActionsService
{
    private readonly string? _connectionString;

    public DbActionsService(IConstants constants)
    {
        _connectionString = constants.ConnectionString;
    }
    
    public Task<int> AddUser(string username, string password, string email, int role)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteUser(string username)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateUser(string username, string password, string email, int role)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUser(string username)
    {
        throw new NotImplementedException();
    }
}
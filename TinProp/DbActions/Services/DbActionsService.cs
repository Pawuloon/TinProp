using System.Data.SQLite;
using Microsoft.AspNetCore.Http.HttpResults;
using TinProp.Constants.Interfaces;
using TinProp.DbActions.Interfaces;
using TinProp.Enums.Roles;
using TinProp.Enums.Statuses;
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
        _connectionString = $"Data Source={constants.ConnectionString};Version=3;";
    }
    
    public async Task<Status> AddUser(string username, string password, string email, int role)
    {
        await using (var connection = new SQLiteConnection(_connectionString))
        {
            try
            {
                var command = new SQLiteCommand(connection);
                command.CommandText = "INSERT INTO APP_USERS (USERNAME, PASSWORD, EMAIL, ROLE) VALUES (@username, @password, @email, @role)";
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@role", role);
                await command.ExecuteNonQueryAsync();
                return Status.Ok;
            }
            catch (Exception e)
            {
               var command = new SQLiteCommand(connection);
               command.CommandText = "INSERT INTO APP_ERRORS (ERROR_CODE, ERROR_MESSAGE) VALUES (@error, @message)";
               command.Parameters.AddWithValue("@error", e.HResult);
               command.Parameters.AddWithValue("@message", e.Message);
            }
        }
        return Status.GeneralError;
    }

    public async Task<Status> DeleteUser(string username)
    {
       await using(var connection = new SQLiteConnection(_connectionString))
       {
           try
           {
               var command = new SQLiteCommand(connection);
               command.CommandText = "DELETE FROM APP_USERS WHERE USERNAME = @username";
               command.Parameters.AddWithValue("@username", username);
               await command.ExecuteNonQueryAsync();
               return Status.Ok;
           }
           catch (Exception e)
           {
               var command = new SQLiteCommand(connection);
               command.CommandText = "INSERT INTO APP_ERRORS (ERROR_CODE, ERROR_MESSAGE) VALUES (@error, @message)";
               command.Parameters.AddWithValue("@error", e.HResult);
               command.Parameters.AddWithValue("@message", e.Message);
           }
       }
       return Status.GeneralError;
    }

    public async Task<Status> UpdateUser(string username, string password, string email, int role)
    {
        await using (var connection = new SQLiteConnection(_connectionString))
        {
            try
            {
                var command = new SQLiteCommand(connection);
                command.CommandText = "UPDATE APP_USERS SET PASSWORD = @password, EMAIL = @email WHERE USERNAME = @username";
                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@username", username);
                await command.ExecuteNonQueryAsync();
                return Status.Ok;
            }
            catch (Exception e)
            {
                var command = new SQLiteCommand(connection);
                command.CommandText = "INSERT INTO APP_ERRORS (ERROR_CODE, ERROR_MESSAGE) VALUES (@error, @message)";
                command.Parameters.AddWithValue("@error", e.HResult);
                command.Parameters.AddWithValue("@message", e.Message);
            }
        }
        return Status.GeneralError;
    }

    public async Task<User> GetUser(string username)
    {
        await using (var connection = new SQLiteConnection(_connectionString))
        {
            try
            {
                var command = new SQLiteCommand(connection);
                command.CommandText = "SELECT * FROM APP_USERS WHERE USERNAME = @username";
                command.Parameters.AddWithValue("@username", username);
                var reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var user = new User(reader.GetString(0), reader.GetString(1), reader.GetString(2), (AppRole) reader.GetInt32(3));
                        return user;
                    }
                }
            }
            catch (Exception e)
            {
                var command = new SQLiteCommand(connection);
                command.CommandText = "INSERT INTO APP_ERRORS (ERROR_CODE, ERROR_MESSAGE) VALUES (@error, @message)";
                command.Parameters.AddWithValue("@error", e.HResult);
                command.Parameters.AddWithValue("@message", e.Message);
            }
        }
        return new User("null", "null", "null", AppRole.User);
    }
}
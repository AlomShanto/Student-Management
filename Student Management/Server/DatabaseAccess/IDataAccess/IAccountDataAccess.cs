using System;
using Server.Contracts.DataTransferObjects;
using Server.Contracts.Entities;
using Server.Contracts.Models;

namespace Server.DatabaseAccess.IDataAccess;

public interface IAccountDataAccess
{
    Task CreateNewUser(UserForm newUser);
    Task<List<UserDto>> GetUsersAsync(string role);
    Task<User> GetUserByUsernameAsync(string username);

    Task<Boolean> UpdateUsername(string  username, string updatedUsername);
    Task<Boolean> DeleteUserAsync(string username);
}

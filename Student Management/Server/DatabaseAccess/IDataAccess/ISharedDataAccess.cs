using System;
using Server.Contracts.Models;

namespace Server.DatabaseAccess.IDataAccess;

public interface ISharedDataAccess
{
    public Task CreateNewUser(UserForm user);
    public Task<bool> DeleteUser(string username);
}

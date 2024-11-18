using System;
using Server.Contracts.Models;

namespace Server.DatabaseAccess.IDataAccess;

public interface IAccountDataAccess
{
    Task CreateNewUser(UserForm newUser);
}

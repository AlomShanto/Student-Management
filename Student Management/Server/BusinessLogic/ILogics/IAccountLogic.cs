using System;
using Server.Contracts.DataTransferObjects;
using Server.Contracts.Models;

namespace Server.BusinessLogic.ILogics;

public interface IAccountLogic
{
    Task CreateUser(UserForm user);
    Task<List<UserDto>> GetUsers(string role);
}

using System;
using Server.Contracts.Models;

namespace Server.BusinessLogic.ILogics;

public interface IAccountLogic
{
    Task CreateUser(UserForm user);
}

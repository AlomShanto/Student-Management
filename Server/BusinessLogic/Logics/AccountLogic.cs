using System;
using Server.BusinessLogic.ILogics;
using Server.Contracts.Models;
using Server.DatabaseAccess.IDataAccess;

namespace Server.BusinessLogic.Logics;

public class AccountLogic : IAccountLogic
{
    private readonly IAccountDataAccess _accountDataAccess;
    private readonly ISharedDataAccess _sharedDataAccess;
    public async Task CreateUser(UserForm user)
    {
        await _accountDataAccess.CreateNewUser(user);
        await _sharedDataAccess.CreateNewUser(user);
        return;
    }
}

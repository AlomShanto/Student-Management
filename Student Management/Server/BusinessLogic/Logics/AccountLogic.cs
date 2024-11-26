using System;
using Server.BusinessLogic.ILogics;
using Server.Contracts.DataTransferObjects;
using Server.Contracts.Models;
using Server.DatabaseAccess.IDataAccess;

namespace Server.BusinessLogic.Logics;

public class AccountLogic : IAccountLogic
{
    private readonly IAccountDataAccess _accountDataAccess;
    private readonly ISharedDataAccess _sharedDataAccess;

    public AccountLogic(IAccountDataAccess accountDataAccess, ISharedDataAccess sharedDataAccess)
    {
        _accountDataAccess = accountDataAccess;
        _sharedDataAccess = sharedDataAccess;
    }

    public async Task CreateUser(UserForm user)
    {
        try
        {
            await _sharedDataAccess.CreateNewUser(user);
            await _accountDataAccess.CreateNewUser(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public async Task<List<UserDto>> GetUsers(string role)
    {
        return await _accountDataAccess.GetUsersAsync(role);
    }
}

using System;
using Microsoft.AspNetCore.Http.HttpResults;
using Server.Contracts.Models;
using Server.DatabaseAccess.IDataAccess;

namespace Server.DatabaseAccess.DataAccess;

public class SharedDataAccess : ISharedDataAccess
{
    private readonly IAccountDataAccess _account;
    private readonly IStudentDataAccess _student;
    private readonly ITeacherDataAccess _teacher;
    public async Task CreateNewUser(UserForm user)
    {
        user.Username = user.Username.ToLower();
        user.Role = user.Role.ToLower();
        if(true){

        }
    }
}

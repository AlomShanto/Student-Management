using System;
using Microsoft.AspNetCore.Http.HttpResults;
using Server.Contracts.Entities;
using Server.Contracts.Models;
using Server.DatabaseAccess.IDataAccess;

namespace Server.DatabaseAccess.DataAccess;

public class SharedDataAccess : ISharedDataAccess
{
    private readonly IAccountDataAccess _account;
    private readonly IStudentDataAccess _student;
    private readonly ITeacherDataAccess _teacher;

    public SharedDataAccess(IAccountDataAccess account, IStudentDataAccess student, ITeacherDataAccess teacher)
    {
        _account = account;
        _student = student;
        _teacher = teacher;
    }
    public async Task CreateNewUser(UserForm user)
    {
        user.Username = user.Username.ToLower();
        user.Role = user.Role.ToLower();
        if(user.Role == "student"){
            Student student = new Student();
            student.Username = user.Username;
            await _student.CreateNewStudentAsync(student);
        }
        else if(user.Role == "teacher"){
            Teacher teacher = new Teacher();
            teacher.Username = user.Username;
            await _teacher.CreateNewTeacherAsync(teacher);
        }
    }

    public async Task<bool> DeleteUser(string username)
    {
        var user = await _account.GetUserByUsernameAsync(username);

        if (user == null)
        {
            throw new Exception("User doesn't exist");
        }

        if (user.Role == "student")
        {
            await _student.DeleteStudentAsync(username);
        }
        else if (user.Role == "teacher")
        {
            await _teacher.DeleteTeacherAsync(username);
        }

        return await _account.DeleteUserAsync(username);

    }
}

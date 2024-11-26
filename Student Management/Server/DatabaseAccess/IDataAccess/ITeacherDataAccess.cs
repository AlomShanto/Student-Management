using System;
using Server.Contracts.Entities;
using Server.Contracts.Models;

namespace Server.DatabaseAccess.IDataAccess;

public interface ITeacherDataAccess
{
    public Task CreateNewTeacherAsync(Teacher teacher);

    public Task<List<Teacher>> GetTeachersAsync();

    public Task<Teacher> GetTeacherByUsernameAsync(string username);

    public Task<Boolean> UpdateTeacherAsync(string username, UpdateTeacher teacher);

    public Task<Boolean> DeleteTeacherAsync(string username);
}

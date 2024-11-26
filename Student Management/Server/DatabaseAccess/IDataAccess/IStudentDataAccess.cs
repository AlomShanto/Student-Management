using System;
using Server.Contracts.DataTransferObjects;
using Server.Contracts.Entities;
using Server.Contracts.Models;

namespace Server.DatabaseAccess.IDataAccess;

public interface IStudentDataAccess
{
    public Task CreateNewStudentAsync(Student student);
    Task<Student> GetStudentByUsernameAsync(string username);
    public Task<List<Student>> GetStudentsAsync();

    Task<Boolean> UpdateStudentAsync(string username, UpdateStudent student);

    Task<Boolean> DeleteStudentAsync(string username);
}

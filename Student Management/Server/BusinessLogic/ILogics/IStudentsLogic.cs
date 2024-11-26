using Server.Contracts.DataTransferObjects;
using Server.Contracts.Entities;
using Server.Contracts.Models;

namespace Server.BusinessLogic.ILogics
{
    public interface IStudentsLogic
    {
        Task<Student> GetStudentByUsername(string username);
        Task<List<Student>> GetStudents();

        Task<Boolean> UpdateStudent(string username, UpdateStudent student);

        Task DeleteStudent(string username);
    }
}

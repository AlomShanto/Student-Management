using Server.Contracts.Entities;
using Server.Contracts.Models;

namespace Server.BusinessLogic.ILogics
{
    public interface ITeachersLogic
    {
        Task<List<Teacher>> GetAllTeachers();
        Task<Teacher> GetTeacherByUsername(string username);

        Task<Boolean> UpdateTeacher(string username, UpdateTeacher teacher);

        Task<bool> DeleteTeacher(string username);
    }
}

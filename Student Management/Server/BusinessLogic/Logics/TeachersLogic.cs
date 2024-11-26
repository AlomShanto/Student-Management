using Server.BusinessLogic.ILogics;
using Server.Contracts.Entities;
using Server.Contracts.Models;
using Server.DatabaseAccess.IDataAccess;

namespace Server.BusinessLogic.Logics
{
    public class TeachersLogic : ITeachersLogic
    {
        private readonly ITeacherDataAccess _teacherDataAccess;
        private readonly ISharedDataAccess _sharedDataAccess;

        public TeachersLogic(ITeacherDataAccess teacherDataAccess, ISharedDataAccess sharedDataAccess)
        {
            _teacherDataAccess = teacherDataAccess;
            _sharedDataAccess = sharedDataAccess;
        }

        public async Task<List<Teacher>> GetAllTeachers()
        {
            return await _teacherDataAccess.GetTeachersAsync();
        }

        public async Task<Teacher> GetTeacherByUsername(string username)
        {
            return await _teacherDataAccess.GetTeacherByUsernameAsync(username);
        }

        public async Task<bool> UpdateTeacher(string username, UpdateTeacher teacher)
        {
            return await _teacherDataAccess.UpdateTeacherAsync(username, teacher);
        }

        public async Task<bool> DeleteTeacher(string username)
        {
            return await _sharedDataAccess.DeleteUser(username);
        }
    }
}

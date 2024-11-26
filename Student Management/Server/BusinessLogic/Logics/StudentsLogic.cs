using Server.BusinessLogic.ILogics;
using Server.Contracts.DataTransferObjects;
using Server.Contracts.Entities;
using Server.Contracts.Models;
using Server.DatabaseAccess.IDataAccess;

namespace Server.BusinessLogic.Logics
{
    public class StudentsLogic : IStudentsLogic
    {
        private readonly IStudentDataAccess _studentDataAccess;
        private readonly ISharedDataAccess _sharedDataAccess;

        public StudentsLogic(IStudentDataAccess studentDataAccess, ISharedDataAccess sharedDataAccess)
        {
            _studentDataAccess = studentDataAccess;
            _sharedDataAccess = sharedDataAccess;
        }

        public async Task<List<Student>> GetStudents()
        {
            return await _studentDataAccess.GetStudentsAsync();
        }

        public async Task<Student> GetStudentByUsername(string username)
        {
            return await _studentDataAccess.GetStudentByUsernameAsync(username);
        }

        public async Task<bool> UpdateStudent(string username, UpdateStudent student)
        {
            return await _studentDataAccess.UpdateStudentAsync(username, student);
        }

        public async Task DeleteStudent(string username)
        {
            await _sharedDataAccess.DeleteUser(username);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Server.BusinessLogic.ILogics;
using Server.Contracts.Models;
using Server.DatabaseAccess.IDataAccess;

namespace Server.Application.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsLogic _studentLogic;

        public StudentsController(IStudentsLogic studentLogic)
        {
            _studentLogic = studentLogic;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            return Ok( await _studentLogic.GetStudents());
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetStudentByUsername(string username)
        {
            return Ok(await _studentLogic.GetStudentByUsername(username));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStudent([FromQuery]string username, [FromBody] UpdateStudent student)
        {
            try
            {
                await _studentLogic.UpdateStudent(username, student);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStudent(string username)
        {
            try
            {
                await _studentLogic.DeleteStudent(username);
                return Ok();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

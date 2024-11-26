using Microsoft.AspNetCore.Mvc;
using Server.BusinessLogic.ILogics;
using Server.BusinessLogic.Logics;
using Server.Contracts.Models;

namespace Server.Application.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class TeachersController : ControllerBase
    {
        private readonly ITeachersLogic _teachersLogic;

        public TeachersController(ITeachersLogic teachersLogic)
        {
            _teachersLogic = teachersLogic;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTeachers()
        {
            return Ok(await _teachersLogic.GetAllTeachers());
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetTeacherByUsername(string username)
        {
            return Ok(await _teachersLogic.GetTeacherByUsername(username));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTeachers([FromQuery] string username, [FromBody] UpdateTeacher teacher)
        {
            try
            {
                await _teachersLogic.UpdateTeacher(username, teacher);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTeacher([FromQuery]string username)
        {
            try
            {
                await _teachersLogic.DeleteTeacher(username);
                return Ok();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

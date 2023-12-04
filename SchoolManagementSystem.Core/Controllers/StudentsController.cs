using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManagementSystem.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        public readonly ILogger<StudentsController> _logger;
        public StudentsController(IServiceProvider serviceProvider) 
        {
            _logger = serviceProvider.GetRequiredService<ILogger<StudentsController>>();
        }


        [HttpGet]
        [Route("{studentId}/get-student")]
        public async Task<IActionResult> GetStudent(Guid StudentId)
        {
            return Ok("Student Id=##########");
        }

        [HttpGet]
        [Route("get-students")]
        public async Task<IActionResult> GetStudents(Guid StudentId)
        {
            return Ok("Student Id=##########");
        }

        [HttpPost]
        [Route("add-student")]
        public async Task<IActionResult> PostStudent(Guid StudentId)
        {
            return Ok("Student Id=##########");
        }

        [HttpPut]
        [Route("modify-student")]
        public async Task<IActionResult> UpdateStudent(Guid StudentId)
        {
            return Ok("Student Id=##########");
        }

        [HttpDelete]
        [Route("remove-student")]
        public async Task<IActionResult> DeleteStudent(Guid StudentId)
        {
            return Ok("Student Id=##########");
        }
    }
}

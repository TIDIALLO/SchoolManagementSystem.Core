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
        [Route("get-student")]
        public async Task<IActionResult> GetStudent(Guid StudentId)
        {
            return Ok("Student Id=##########");
        }
    }
}

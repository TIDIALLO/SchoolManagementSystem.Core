using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.DAL;
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Portal.Shared.Request;

namespace SchoolManagementSystem.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ILogger<StudentsController> _logger;
        private readonly ApplicationDbContext _dbcontext;

        public StudentsController(IServiceProvider serviceProvider) 
        {
            _logger = serviceProvider.GetRequiredService<ILogger<StudentsController>>();
            _dbcontext = serviceProvider.GetRequiredService<ApplicationDbContext>();
        }


        [HttpGet]
        [Route("get-student/{id}")]
        public async Task<IActionResult> GetStudent(Guid id)
        {
            var student = await _dbcontext.Students.FirstOrDefaultAsync(u=> u.StudentId == id);
            if (student == null) return NotFound( "Not Found");

            return Ok(student);
        }

        [HttpGet]
        [Route("get-students")]
        public async Task<IActionResult> GetStudents()
        {
            return Ok("Student Id=##########");
        }

        [HttpPost]
        [Route("save-student")]
        public async Task<IActionResult> SaveStudent(SaveStudentRequest request)
        {
            var entity = new StudentEntity
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                DateOfBirth = request.DateOfBirth,
                Address = request.Address,
                Enrollments = request.Enrollments
            };
            await _dbcontext.Students.AddAsync(entity);
            await _dbcontext.SaveChangesAsync();

            return Ok(request);
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

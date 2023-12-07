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
            if (student == null) return NotFound("student Not Found");

            return Ok(student);
        }


        //get students
        [HttpGet]
        [Route("get-students")]
        public async Task<ActionResult<IEnumerable<StudentEntity>>> GetStudents()
        {
            return await _dbcontext.Students.ToListAsync();
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
        [Route("update-student/{id}")]
        public async Task<IActionResult> UpdateStudent(Guid id, StudentEntity student)
        {
            if(id != student.StudentId)
            {
                return BadRequest();
            }
            _dbcontext.Entry(student).State = EntityState.Modified;

            try
            {
                await _dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();

        }

        [HttpDelete]
        [Route("remove-student/{id}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            var student = await _dbcontext.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            _dbcontext.Students.Remove(student);
            await _dbcontext.SaveChangesAsync();

            return Ok(student);
        }



        private bool StudentExists(Guid id)
        {
            return _dbcontext.Students.Any(e => e.StudentId == id);
        }

    }

    
}

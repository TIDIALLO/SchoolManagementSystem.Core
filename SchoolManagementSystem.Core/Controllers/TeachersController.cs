using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Core.Api.cqrs.Queries.TeacherQueries;
using SchoolManagementSystem.DAL;
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Portal.Shared.Request;
using static SchoolManagementSystem.Core.Api.cqrs.Commands.StudentCommands.StudentCommands;
using static SchoolManagementSystem.Core.Api.cqrs.Commands.TeacherCommands.TeacherCommands;

namespace SchoolManagementSystem.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        public readonly ILogger<TeachersController> _logger;
        private readonly IMediator _mediator;

        public TeachersController(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<ILogger<TeachersController>>();
            _mediator = serviceProvider.GetRequiredService<IMediator>();
        }


        //Save Teacher
        [HttpPost]
        [Route("save-teacher")]
        public async Task<IActionResult> SaveTeacher(SaveTeacherRequest request)
        {
            var saveRequest = new SaveTeacherCommand(request);
            var result = await _mediator.Send(saveRequest);

            return Ok(request);
            /*            var entity = new TeacherEntity
                        {
                            FirstName = request.FirstName,
                            LastName = request.LastName,
                            Subject = request.Subject,
                            Email = request.Email,
                            Courses = request.Courses,
                        };
                        await _dbContext.Teacher.AddAsync(entity);
                        await _dbContext.SaveChangesAsync();

                        return Ok(request);*/
        }

        // get Teacher
        [HttpGet]
        [Route("get-teacher/{id}")]
        public async Task<ActionResult<IEnumerable<TeacherEntity>>> GetTeacher(Guid id)
        {
            var result = await _mediator.Send(new TeacherQueries.GetTeacherQuery(id));
            if (result == null) return NotFound($"Student with id '{id}' cannot be found!");
            return Ok(result);
            /*var teacher = await _dbContext.Teacher.FirstOrDefaultAsync(u => u.TeacherId == id);
            if (teacher == null) return NotFound("Teacher Not Found");

            return Ok(teacher);
            //return await _dbContext.Teacher.ToListAsync();*/
        }

        //get teachers
        [HttpGet]
        [Route("get-teachers")]
        public async Task<ActionResult<IEnumerable<TeacherEntity>>> GetTeachers()
        {
            var result = await _mediator.Send(new TeacherQueries.GetAllTeacherQuery());
            return Ok(result);
            // return await _dbContext.Teacher.ToListAsync();
        }




        //update teacher
        [HttpPut]
        [Route("update-teacher/{id}")]
        public async Task<IActionResult> TeacherStudent(Guid id, SaveTeacherRequest request)
        {
            /*if (id != teacher.TeacherId)
            {
                return BadRequest();
            }
            _dbContext.Entry(teacher).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }*/
            var result = await _mediator.Send(new UpdateTeacherCommand(request));
            return Ok(result);
            //return NoContent();
        }

        //remove Student
        [HttpDelete]
        [Route("remove-teacher/{id}")]
        public async Task<IActionResult> RemoveTeacher(Guid id)
        {
            var result = await _mediator.Send(new DeleteStudentCommand(id));
            return Ok(result);
            /*  var teacher = await _dbContext.Teacher.FindAsync(id);
              if (teacher == null)
              {
                  return NotFound();
              }
              _dbContext.Teacher.Remove(teacher);
              await _dbContext.SaveChangesAsync();

              return Ok(teacher);*/
        }


    }
}

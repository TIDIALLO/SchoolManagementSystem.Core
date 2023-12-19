using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Core.Api.cqrs.Queries.CourseQueries;
using SchoolManagementSystem.DAL;
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Portal.Shared.Request;
using static SchoolManagementSystem.Core.Api.cqrs.Commands.CourseCommands.CourseCommands;
using static SchoolManagementSystem.Core.Api.cqrs.Commands.StudentCommands.StudentCommands;

namespace SchoolManagementSystem.Core.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
    public readonly ILogger<CoursesController> _logger;
    private readonly ApplicationDbContext _dbContext;
    private readonly IMediator _mediator;

    public CoursesController(IServiceProvider serviceProvider)
    {
        _logger = serviceProvider.GetRequiredService<ILogger<CoursesController>>();
        _mediator = serviceProvider.GetRequiredService<IMediator>();
        _dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
    }

    //Save courses
    [HttpPost]
    [Route("save-course")]
    public async Task<IActionResult> SaveCourses(SaveCourseRequest request)
    {
        {
            var saveRequest = new SaveCourseCommand(request);
            var result = await _mediator.Send(saveRequest);

            return Ok(request);
            /*   var entity = new CourseEntity
               {
                   Title = request.Title,
                   Description = request.Description,
                   TeacherId = request.TeacherId,
                   Teacher = request.Teacher,
                   Enrollments = request.Enrollments
               };
               await _dbContext.Courses.AddAsync(entity);
               await _dbContext.SaveChangesAsync();

               return Ok(request);*/
        }
    }
    //get course by Id
    [HttpGet]
    [Route("get-course/{id}")]
    public async Task<IActionResult> GetCourse(Guid id)
    {
        var course = await _dbContext.Courses.FirstOrDefaultAsync(u => u.Id == id);
        if (course == null) return NotFound("course Not Found");

        return Ok(course);
    }

    //get courses
    [HttpGet]
    [Route("get-courses")]
   /* public async Task<ActionResult<IEnumerable<CourseEntity>>> GetCourses()
    {
        var result = await _mediator.Send(new CourseQueries.GetCourseQuery());
        return Ok(result);
    }*/


  

    //Update Student
    [HttpPut]
    [Route("update-course/{id}")]
 /*   public async Task<IActionResult> UpdateCourse(Guid id, CourseEntity course)
    {
        if (id != course.CourseId)
        {
            return BadRequest();
        }
        _dbContext.Entry(course).State = EntityState.Modified;

        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CourseExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return NoContent();
    }*/


    //remove course
    [HttpDelete]
    [Route("remove-course/{id}")]
    public async Task<IActionResult> DeleteCourse(Guid id)
    {
        var course = await _dbContext.Courses.FindAsync(id);
        if (course == null)
        {
            return NotFound();
        }
        _dbContext.Courses.Remove(course);
        await _dbContext.SaveChangesAsync();

        return Ok(course);
    }

    private bool CourseExists(Guid id)
    {
        return _dbContext.Courses.Any(e => e.Id == id);
    }


}

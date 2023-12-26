using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Core.Api.cqrs.Queries.CourseQueries;
using SchoolManagementSystem.Core.Api.cqrs.Queries.StudentQueries;
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
        var saveRequest = new SaveCourseCommand(request);
        var result = await _mediator.Send(saveRequest);
        return Ok(request);
    }
    /// <summary>
    /// get course by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("get-course/{id}")]
    public async Task<IActionResult> GetCourse(Guid id)
    {
        var result = await _mediator.Send(new StudentQueries.GetStudentQuery(id));
        if (result == null) return NotFound($"Student with id '{id}' cannot be found!");
        return Ok(result);
    }

    /// <summary>
    /// get courses
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("get-courses")]
    public async Task<ActionResult<IEnumerable<CourseEntity>>> GetCourses()
    {
        var result = await _mediator.Send(new StudentQueries.GetAllStudentQuery());
        if (result == null) return NotFound("result Not Found");
        return Ok(result);
    }


  

    /// <summary>
    /// Update Student
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("update-course/{id}")]
    public async Task<IActionResult> UpdateCourse(Guid id, SaveStudentRequest request)
    {
        var result = await _mediator.Send(new UpdateStudentCommand(request));
        return Ok(result);
    }


    /// <summary>
    /// remove course
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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

}

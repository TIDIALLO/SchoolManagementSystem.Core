using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Core.Api.cqrs.Queries.EnrollmentQueries;
using SchoolManagementSystem.DAL;
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Portal.Shared.Request;
using static SchoolManagementSystem.Core.Api.cqrs.Commands.EnrollmentCommands.EnrollmentCommands;

namespace SchoolManagementSystem.Core.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EnrollmentsController : ControllerBase
{
    public readonly ILogger<EnrollmentsController> _logger;
    private readonly ApplicationDbContext _dbContext;
    private readonly IMediator _mediator;

    public EnrollmentsController(IServiceProvider serviceProvider)
    {
        _logger = serviceProvider.GetRequiredService<ILogger<EnrollmentsController>>();
        _mediator = serviceProvider.GetRequiredService<IMediator>();

        _dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
    }


    //Save enrollment
    [HttpPost]
    [Route("save-enrollment")]
    public async Task<IActionResult> SaveEnrollment(SaveEnrollmentRequest request)
    {
        var saveRequest = new SaveEnrollmentCommand(request);
        var result = await _mediator.Send(saveRequest);

        return Ok(result);
        /*   var entity = new EnrollmentEntity
           {
               StudentId = request.StudentId,
               CourseId = request.CourseId,
               EnrollmentDate = request.EnrollmentDate,
               Student = request.Student,
               Course = request.Course,
           };
           await _dbContext.Enrollments.AddAsync(entity);
           await _dbContext.SaveChangesAsync();

           return Ok(request);*/
    }

    //get enrollment by Id
    [HttpGet]
    [Route("get-enrollment/{id}")]
    public async Task<IActionResult> GetEnrollment(Guid id)
    {
        var enrollment = await _dbContext.Enrollments.FirstOrDefaultAsync(u => u.Id == id);
        if (enrollment == null) return NotFound("enrollment Not Found");

        return Ok(enrollment);
    }

    //get enrollments
    [HttpGet]
    [Route("get-enrollments")]
   public async Task<ActionResult<IEnumerable<EnrollmentEntity>>> GetEnrollments()
    {
        var result = await _mediator.Send(new EnrollmentQueries.GetAllEnrollmentQuery());
        if (result == null) return NotFound("result Not Found");
        return Ok(result);
        //return await _dbContext.Enrollments.ToListAsync();
    }

   

    //update enrollment
    [HttpPut]
    [Route("update-enrollment/{id}")]
    public async Task<IActionResult> UpdateEnrollment(Guid id, EnrollmentEntity enrollment)
    {
        if (id != enrollment.Id)
        {
            return BadRequest();
        }
        _dbContext.Entry(enrollment).State = EntityState.Modified;

        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EnrollmentExists(id))
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

    private bool EnrollmentExists(Guid id)
    {
        return _dbContext.Enrollments.Any(e => e.Id == id);
    }



    //remove Student
    [HttpDelete]
    [Route("remove-enrollment/{id}")]
    public async Task<IActionResult> DeleteEnrollment(Guid id)
    {
        var enrollment = await _dbContext.Enrollments.FindAsync(id);
        if (enrollment == null)
        {
            return NotFound();
        }
        _dbContext.Enrollments.Remove(enrollment);
        await _dbContext.SaveChangesAsync();

        return Ok(enrollment);
    }


}

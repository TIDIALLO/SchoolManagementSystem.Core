using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Core.Api.cqrs.Queries.EnrollmentQueries;
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Portal.Shared.Request;
using static SchoolManagementSystem.Core.Api.cqrs.Commands.EnrollmentCommands.EnrollmentCommands;
using static SchoolManagementSystem.Core.Api.cqrs.Commands.StudentCommands.StudentCommands;

namespace SchoolManagementSystem.Core.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EnrollmentsController : ControllerBase
{
    public readonly ILogger<EnrollmentsController> _logger;
    private readonly IMediator _mediator;

    public EnrollmentsController(IServiceProvider serviceProvider)
    {
        _logger = serviceProvider.GetRequiredService<ILogger<EnrollmentsController>>();
        _mediator = serviceProvider.GetRequiredService<IMediator>();
    }


    /// <summary>
    /// Save enrollment
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("save-enrollment")]
    public async Task<IActionResult> SaveEnrollment(SaveEnrollmentRequest request)
    {
        var saveRequest = new SaveEnrollmentCommand(request);
        var result = await _mediator.Send(saveRequest);

        return Ok(result);

    }

    /// <summary>
    /// get enrollment by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("get-enrollment/{id}")]
    public async Task<IActionResult> GetEnrollment(Guid id)
    {
        var result = await _mediator.Send(new EnrollmentQueries.GetEnrollmentQuery(id));
        if (result == null) return NotFound($"Enrollment with id '{id}' cannot be found!");
        return Ok(result);
    }

    /// <summary>
    /// get enrollments
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("get-enrollments")]
   public async Task<ActionResult<IEnumerable<EnrollmentEntity>>> GetEnrollments()
    {
        var result = await _mediator.Send(new EnrollmentQueries.GetAllEnrollmentQuery());
        if (result == null) return NotFound("result Not Found");
        return Ok(result);
   }
   
    /// <summary>
    /// update enrollment
    /// </summary>
    /// <param name="id"></param>
    /// <param name="enrollment"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("update-enrollment/{id}")]
    public async Task<IActionResult> UpdateEnrollment(Guid id, SaveEnrollmentRequest request)
    {
        var result = await _mediator.Send(new UpdateEnrollmentCommand(request));
        return Ok(result);
    }


    /// <summary>
    /// remove Student
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("remove-enrollment/{id}")]
    public async Task<IActionResult> DeleteEnrollment(Guid id)
    {
        var result = await _mediator.Send(new DeleteStudentCommand(id));
        if (result == null) return NotFound("result Not Found");
        return Ok(result);
    }

}

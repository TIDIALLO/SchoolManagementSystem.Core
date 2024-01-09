using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Core.Api.cqrs.Queries.StudentQueries;


//using SchoolManagementSystem.Application.Student.Commands.CreateStudentCommand;
using SchoolManagementSystem.DAL;
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Portal.Shared.Request;
using static SchoolManagementSystem.Core.Api.cqrs.Commands.StudentCommands.StudentCommands;
namespace SchoolManagementSystem.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(IServiceProvider serviceProvider) 
        {
            _logger = serviceProvider.GetRequiredService<ILogger<StudentsController>>();
            _mediator = serviceProvider.GetRequiredService<IMediator>();
        }

        /// <summary>
        /// Save Student
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("save-student")]
        public async Task<IActionResult> SaveStudent(SaveStudentRequest request)
        {
            var saveRequest = new SaveStudentCommand(request);
            var result = await _mediator.Send(saveRequest);

            return Ok(request);
        }


        /// <summary>
        /// get Student by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-student/{id}")]
        public async Task<IActionResult> GetStudentById(Guid id)
        {
            var result = await _mediator.Send(new StudentQueries.GetStudentQuery(id));
            if (result == null) return NotFound($"Student with id '{id}' cannot be found!");
            return Ok(result);
          
        }

        /// <summary>
        /// get all students
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get-students")]
        public async Task<ActionResult<IEnumerable<StudentEntity>>> GetAllStudents()
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
        [Route("update-student/{id}")]
        public async Task<IActionResult> UpdateStudent(Guid id, SaveStudentRequest request)
        {
            var result = await _mediator.Send(new UpdateStudentCommand(request));
            return Ok(result);
        }

        /// <summary>
        /// remove Student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("remove-student/{id}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            var result = await _mediator.Send(new DeleteStudentCommand(id));
            if (result == null) return NotFound("result Not Found");
            return Ok(result);
        }

    }

    
}

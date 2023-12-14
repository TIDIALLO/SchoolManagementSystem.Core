using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<StudentsController> _logger;
        // private readonly IStudentRepository _studentRepository;

        public StudentsController(IServiceProvider serviceProvider) 
        {
            _logger = serviceProvider.GetRequiredService<ILogger<StudentsController>>();
            _mediator = serviceProvider.GetRequiredService<IMediator>();
            _dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            //  _studentRepository = serviceProvider.GetRequiredService<IStudentRepository>();

        }

        //#######################    using Mapper and repository   ########################
        /*        [HttpPost]
                public async Task<ActionResult<StudentEntity>> CreateCategory(SaveStudentRequest request)
                {
                    var e = _mapper.Map<SaveStudentRequest>(request);

                    await _studentRepository.SaveStudent(request);

                    return CreatedAtAction("GetCategory", new { id = e.id }, e);
                }*/

        //################################################################################


        //Save Student
        [HttpPost]
        [Route("save-student")]
        public async Task<IActionResult> SaveStudent(SaveStudentRequest request)
        {
            var saveRequest = new SaveStudentCommand(request);
            var result = await _mediator.Send(saveRequest);

            return Ok(request);
        }


        //get Student by Id
        [HttpGet]
        [Route("get-student/{id}")]
        public async Task<IActionResult> GetStudentById(Guid id)
        {
            var result = await _mediator.Send(new CourseQueries.GetStudentQuery(id));
            if (result == null) return NotFound($"Student with id '{id}' cannot be found!");
            return Ok(result);
          
        }

        //get all students
        [HttpGet]
        [Route("get-students")]
        public async Task<ActionResult<IEnumerable<StudentEntity>>> GetAllStudents()
        {
            var result = await _mediator.Send(new CourseQueries.GetAllStudentQuery());
            if (result == null) return NotFound("result Not Found");
            return Ok(result);
        }

        //Update Student
        [HttpPut]
        [Route("update-student/{id}")]
        public async Task<IActionResult> UpdateStudent(Guid id, SaveStudentRequest request)
        {
            var result = await _mediator.Send(new UpdateStudentCommand(request));
            if (result == null) return NotFound("result Not Found");

            return Ok(result);
        }

        //remove Student
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

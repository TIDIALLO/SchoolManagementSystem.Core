using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Core.Api.cqrs.Queries.StudentQueries;
using SchoolManagementSystem.Core.Api.cqrs.Queries.TeacherQueries;
using SchoolManagementSystem.DAL;
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Portal.Shared.Request;
using SchoolManagementSystem.Portal.Shared.Response;
using SchoolManagementSystem.Portal.Shared.Result;
<<<<<<< HEAD
using static SchoolManagementSystem.Core.Api.cqrs.Commands.StudentCommands.StudentCommands;
=======
>>>>>>> 3f77ab6d5fbfdd89f8dc181cd2e9753a31a00a74
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


        /// <summary>
        /// Save Teacher
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("save-teacher")]
        public async Task<ActionResult<SaveTeacherResponse>> SaveTeacher(SaveTeacherRequest request)
        {
            var saveRequest = new SaveTeacherCommand(request);
            var result = await _mediator.Send(saveRequest);
            return Ok(await Result<SaveTeacherResponse>.SuccessAsync(result));
        }

        /// <summary>
        /// get Teacher
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-teacher/{id}")]
        public async Task<ActionResult<SaveTeacherRequest>>  GetTeacherById(Guid id)
        {
            var result = await _mediator.Send(new TeacherQueries.GetTeacherQuery(id));
            if (result == null) return NotFound($"Teacher with id '{id}' cannot be found!");
            return Ok(await Result<SaveTeacherRequest>.SuccessAsync(result));
        }

        /// <summary>
        /// get teachers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get-teachers")]
        public async Task<ActionResult<IList<TeacherEntity>>> GetTeachers()
        {
            var result = await _mediator.Send(new TeacherQueries.GetAllTeacherQuery());
            if (result == null) return NotFound("result Not Found");
            return Ok(await Result<IList<SaveTeacherResponse>>.SuccessAsync(result));
        }

        /// <summary>
        /// update teacher
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update-teacher/{id}")]
        public async Task<IActionResult> TeacherStudent(SaveTeacherRequest request)
        {
            var result = await _mediator.Send(new UpdateTeacherCommand(request));
            return Ok(await Result<SaveTeacherResponse>.SuccessAsync(result));
        }

        /// <summary>
        /// remove Student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("remove-teacher/{id}")]
        public async Task<ActionResult<SaveStudentResponse>> RemoveTeacher(Guid id)
        {
            var result = await _mediator.Send(new DeleteTeacherCommand(id));
            if (result == null) return NotFound("result Not Found");
            return Ok(await Result<SaveTeacherResponse>.SuccessAsync(result));
        }


    }
}

using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Core.Api.Contracts;
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Portal.Shared.Request;

namespace SchoolManagementSystem.Core.Api.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        public Task<IActionResult> DeleteCourse(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<IEnumerable<CourseEntity>>> GetAllCourses()
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> GetTeacherById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> SaveCourse(SaveCourseRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> UpdateCourse(Guid id, CourseEntity course)
        {
            throw new NotImplementedException();
        }
    }
}

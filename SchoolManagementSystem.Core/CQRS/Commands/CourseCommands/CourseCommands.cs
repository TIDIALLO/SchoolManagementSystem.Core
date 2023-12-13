using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.DAL;
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Portal.Shared.Request;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagementSystem.Portal.Shared.Response;

namespace SchoolManagementSystem.Core.Api.cqrs.Commands.CourseCommands;

public static class CourseCommands
{
    #region SaveCourse
    public class SaveCourseCommand : IRequest<SaveCourseResponse>
    {
        public SaveCourseCommand(SaveCourseRequest course) => Course = course;
        public SaveCourseRequest Course { get; set; }
    }

    public sealed class SaveCourseeCommandHandler : IRequestHandler<SaveCourseCommand, SaveCourseResponse>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public SaveCourseeCommandHandler(IServiceProvider serviceProvider)
        {
            _dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
        }

        public async Task<SaveCourseResponse> Handle(SaveCourseCommand command, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<CourseEntity>(command.Course);

            await _dbContext.Courses.AddAsync(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var persited = await _dbContext.Courses.FirstOrDefaultAsync(s => s.CourseId == entity.CourseId);

            return _mapper.Map<SaveCourseResponse>(persited);
        }
    }

     #endregion

    #region  UpdateCourse
    public class UpdateCourseCommand : IRequest<SaveCourseResponse>
    {
        public UpdateCourseCommand(SaveCourseRequest Course)
        {
            Course = Course;
        }

        public SaveCourseRequest Course { get; set; }

        public sealed class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, SaveCourseResponse>
        {
            private readonly ApplicationDbContext _dbContext;
            private readonly IMapper _mapper;

            public UpdateCourseCommandHandler(IServiceProvider serviceProvider)
            {
                _dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
                _mapper = serviceProvider.GetRequiredService<IMapper>();
            }

            public async Task<SaveCourseResponse> Handle(UpdateCourseCommand command, CancellationToken cancellationToken)
            {
                var entity = _mapper.Map<CourseEntity>(command.Course);

                // Check if the mapped entity is null
                /*if (entity == null)
                {
                    return null;
                }*/

                _dbContext.Entry(entity).State = EntityState.Modified;

                await _dbContext.SaveChangesAsync(cancellationToken);

                // Map the updated entity back to SaveCourseResponse
                var updatedResponse = _mapper.Map<SaveCourseResponse>(entity);

                return updatedResponse;
            }
        }
    }

    #endregion

    #region  DeleteCourse
    public class DeleteCourseCommand : IRequest<SaveCourseResponse>
    {
        public DeleteCourseCommand(Guid CourseId)
        {
            CourseId = CourseId;
        }
        public Guid CourseId { get; }

        //public SaveCourseRequest Course { get; set; }

        public sealed class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, SaveCourseResponse>
        {
            private readonly ApplicationDbContext _dbContext;
            private readonly IMapper _mapper;

            public DeleteCourseCommandHandler(IServiceProvider serviceProvider)
            {
                _dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
                _mapper = serviceProvider.GetRequiredService<IMapper>();
            }

            public async Task<SaveCourseResponse> Handle(DeleteCourseCommand command, CancellationToken cancellationToken)
            {
                
                var courseToRemove = await _dbContext.Courses.FindAsync(command.CourseId);

                if (courseToRemove == null)
                {
                    return null;
                }

                var deletedResponse = _mapper.Map<SaveCourseResponse>(courseToRemove);

                _dbContext.Courses.Remove(courseToRemove);
                await _dbContext.SaveChangesAsync();

                return deletedResponse;
            }

        }

    }

    #endregion
}

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.DAL;
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Portal.Shared.Request;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagementSystem.Portal.Shared.Response;
using static Dapper.SqlMapper;

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

            var persited = await _dbContext.Courses.FirstOrDefaultAsync(s => s.Id == entity.Id);

            return _mapper.Map<SaveCourseResponse>(persited);
        }
    }

    #endregion
    
    #region  UpdateCourse
    public class UpdateCourseCommand : IRequest<SaveCourseResponse>
    {
        public UpdateCourseCommand(SaveCourseRequest course)
        {
            Course = course;
        }

        public SaveCourseRequest Course { get; set; }

        public sealed class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, SaveCourseResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public UpdateCourseCommandHandler(IServiceProvider serviceProvider)
            {
                _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
                _mapper = serviceProvider.GetRequiredService<IMapper>();
            }

            public async Task<SaveCourseResponse> Handle(UpdateCourseCommand command, CancellationToken cancellationToken)
            {
                var entity = _mapper.Map<CourseEntity>(command.Course);
                await _unitOfWork.Courses.UpdateAsync(entity);
                await _unitOfWork.Commit();

                return _mapper.Map<SaveCourseResponse>(entity); ;
            }
        }
    }

    #endregion

    #region  DeleteCourse
    public class DeleteCourseCommand : IRequest<SaveCourseResponse>
    {
        public DeleteCourseCommand(Guid id)
        {
            CourseId = id;
        }
        public Guid CourseId { get; set; }
        public sealed class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, SaveCourseResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public DeleteCourseCommandHandler(IServiceProvider serviceProvider)
            {
                _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
                _mapper = serviceProvider.GetRequiredService<IMapper>();
            }

            public async Task<SaveCourseResponse> Handle(DeleteCourseCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var entity = await _unitOfWork.Courses.GetByIdAsync(command.CourseId);
                    await _unitOfWork.Courses.RemoveAsync(entity);
                    await _unitOfWork.Commit();

                    return _mapper.Map<SaveCourseResponse>(entity);
                }
                catch (NullReferenceException ex)
                {
                    throw new NullReferenceException($"Course not Found {ex.Message}");
                }

            }

        }

    }
    #endregion
}

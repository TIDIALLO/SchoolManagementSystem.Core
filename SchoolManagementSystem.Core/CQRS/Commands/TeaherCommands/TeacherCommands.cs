using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.DAL;
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Portal.Shared.Request;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagementSystem.Portal.Shared.Response;

namespace SchoolManagementSystem.Core.Api.cqrs.Commands.TeacherCommands;

public static class TeacherCommands
{
    #region SaveTeacher
    public class SaveTeacherCommand : IRequest<SaveTeacherResponse>
    {
        public SaveTeacherCommand(SaveTeacherRequest teacher) => Teacher = teacher;
        public SaveTeacherRequest Teacher { get; set; }
    }

    public sealed class SaveTeacherCommandHandler : IRequestHandler<SaveTeacherCommand, SaveTeacherResponse>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public SaveTeacherCommandHandler(IServiceProvider serviceProvider)
        {
            _dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
        }

        public async Task<SaveTeacherResponse> Handle(SaveTeacherCommand command, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TeacherEntity>(command.Teacher);

            await _dbContext.Teacher.AddAsync(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var persited = await _dbContext.Teacher.FirstOrDefaultAsync(s => s.TeacherId == entity.TeacherId);

            return _mapper.Map<SaveTeacherResponse>(persited);
        }
    }

    #endregion

    #region  UpdateTeacher
    public class UpdateTeacherCommand : IRequest<SaveTeacherResponse>
    {
        public UpdateTeacherCommand(SaveTeacherRequest teacher)
        {
            Teacher = teacher;
        }

        public SaveTeacherRequest Teacher { get; set; }

        public sealed class UpdateTeacherCommandHandler : IRequestHandler<UpdateTeacherCommand, SaveTeacherResponse>
        {
            private readonly ApplicationDbContext _dbContext;
            private readonly IMapper _mapper;

            public UpdateTeacherCommandHandler(IServiceProvider serviceProvider)
            {
                _dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
                _mapper = serviceProvider.GetRequiredService<IMapper>();
            }

            public async Task<SaveTeacherResponse> Handle(UpdateTeacherCommand command, CancellationToken cancellationToken)
            {
                var entity = _mapper.Map<TeacherEntity>(command.Teacher);

                // Check if the mapped entity is null
                if (entity == null)
                {
                    return null;
                }

                _dbContext.Entry(entity).State = EntityState.Modified;

                await _dbContext.SaveChangesAsync(cancellationToken);

                // Map the updated entity back to SaveTeacherResponse
                var updatedResponse = _mapper.Map<SaveTeacherResponse>(entity);

                return updatedResponse;
            }
        }
    }

    #endregion

    #region  DeleteTeacher
    public class DeleteTeacherCommand : IRequest<SaveTeacherResponse>
    {
        public DeleteTeacherCommand(Guid teacherId)
        {
            TeacherId = teacherId;
        }
        public Guid TeacherId { get; }

        //public SaveTeacherRequest Teacher { get; set; }

        public sealed class DeleteTeacherCommandHandler : IRequestHandler<DeleteTeacherCommand, SaveTeacherResponse>
        {
            private readonly ApplicationDbContext _dbContext;
            private readonly IMapper _mapper;

            public DeleteTeacherCommandHandler(IServiceProvider serviceProvider)
            {
                _dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
                _mapper = serviceProvider.GetRequiredService<IMapper>();
            }

            public async Task<SaveTeacherResponse> Handle(DeleteTeacherCommand command, CancellationToken cancellationToken)
            {
                
                var teacherToRemove = await _dbContext.Teacher.FindAsync(command.TeacherId);

                if (teacherToRemove == null)
                {
                    return null;
                }

                var deletedResponse = _mapper.Map<SaveTeacherResponse>(teacherToRemove);

                _dbContext.Teacher.Remove(teacherToRemove);
                await _dbContext.SaveChangesAsync();

                return deletedResponse;
            }

            }

    }

    #endregion
}

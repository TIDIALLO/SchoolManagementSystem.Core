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

            var persited = await _dbContext.Teacher.FirstOrDefaultAsync(s => s.Id == entity.Id);

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
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public UpdateTeacherCommandHandler(IServiceProvider serviceProvider)
            {
                _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
                _mapper = serviceProvider.GetRequiredService<IMapper>();
            }

            public async Task<SaveTeacherResponse> Handle(UpdateTeacherCommand command, CancellationToken cancellationToken)
            {
                var entity = _mapper.Map<TeacherEntity>(command.Teacher);
                await _unitOfWork.Teachers.UpdateAsync(entity);
                _unitOfWork.Commit();

                return _mapper.Map<SaveTeacherResponse>(entity); ;
            }
        }
    }
    #endregion

    #region  DeleteTeacher
    public class DeleteTeacherCommand : IRequest<SaveTeacherResponse>
    {
        public DeleteTeacherCommand(Guid id)
        {
            TeacherId = id;
        }
        public Guid TeacherId { get; set; }

        //public SaveTeacherRequest Teacher { get; set; }

        public sealed class DeleteTeacherCommandHandler : IRequestHandler<DeleteTeacherCommand, SaveTeacherResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public DeleteTeacherCommandHandler(IServiceProvider serviceProvider)
            {
                _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
                _mapper = serviceProvider.GetRequiredService<IMapper>();
            }

            public async Task<SaveTeacherResponse> Handle(DeleteTeacherCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var entity = await _unitOfWork.Teachers.GetByIdAsync(command.TeacherId);
                    await _unitOfWork.Teachers.RemoveAsync(entity);
                    _unitOfWork.Commit();

                    return _mapper.Map<SaveTeacherResponse>(entity);
                }
                catch (NullReferenceException ex)
                {
                    throw new NullReferenceException($"Teacher not Found {ex.Message}");
                }

            }

        }

    }
    #endregion
}

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.DAL;
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Portal.Shared.Request;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagementSystem.Portal.Shared.Response;

namespace SchoolManagementSystem.Core.Api.cqrs.Commands.StudentCommands;

public static class StudentCommands
{
    #region SaveStudent
    public class SaveStudentCommand : IRequest<SaveStudentResponse>
    {
        public SaveStudentCommand(SaveStudentRequest student) => Student = student;
        public SaveStudentRequest Student { get; set; }
    }

    public sealed class SaveStudentCommandHandler : IRequestHandler<SaveStudentCommand, SaveStudentResponse>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public SaveStudentCommandHandler(IServiceProvider serviceProvider)
        {
            _dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
        }

        public async Task<SaveStudentResponse> Handle(SaveStudentCommand command, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<StudentEntity>(command.Student);

            await _dbContext.Students.AddAsync(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var persited = await _dbContext.Students.FirstOrDefaultAsync(s => s.StudentId == entity.StudentId);

            return _mapper.Map<SaveStudentResponse>(persited);
        }
    }

    #endregion

    #region  UpdateStudent
    public class UpdateStudentCommand : IRequest<SaveStudentResponse>
    {
        public UpdateStudentCommand(SaveStudentRequest student)
        {
            Student = student;
        }

        public SaveStudentRequest Student { get; set; }

        public sealed class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, SaveStudentResponse>
        {
            private readonly ApplicationDbContext _dbContext;
            private readonly IMapper _mapper;

            public UpdateStudentCommandHandler(IServiceProvider serviceProvider)
            {
                _dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
                _mapper = serviceProvider.GetRequiredService<IMapper>();
            }

            public async Task<SaveStudentResponse> Handle(UpdateStudentCommand command, CancellationToken cancellationToken)
            {
                var entity = _mapper.Map<StudentEntity>(command.Student);

                // Check if the mapped entity is null
                if (entity == null)
                {
                    return null;
                }

                _dbContext.Entry(entity).State = EntityState.Modified;

                await _dbContext.SaveChangesAsync(cancellationToken);

                // Map the updated entity back to SaveStudentResponse
                var updatedResponse = _mapper.Map<SaveStudentResponse>(entity);

                return updatedResponse;
            }
        }
    }

    #endregion

    #region  DeleteStudent
    public class DeleteStudentCommand : IRequest<SaveStudentResponse>
    {
        public DeleteStudentCommand(Guid studentId)
        {
            StudentId = studentId;
        }
        public Guid StudentId { get; }

        //public SaveStudentRequest Student { get; set; }

        public sealed class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, SaveStudentResponse>
        {
            private readonly ApplicationDbContext _dbContext;
            private readonly IMapper _mapper;

            public DeleteStudentCommandHandler(IServiceProvider serviceProvider)
            {
                _dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
                _mapper = serviceProvider.GetRequiredService<IMapper>();
            }

            public async Task<SaveStudentResponse> Handle(DeleteStudentCommand command, CancellationToken cancellationToken)
            {
                
                var studentToRemove = await _dbContext.Students.FindAsync(command.StudentId);

                if (studentToRemove == null)
                {
                    return null;
                }

                var deletedResponse = _mapper.Map<SaveStudentResponse>(studentToRemove);

                _dbContext.Students.Remove(studentToRemove);
                await _dbContext.SaveChangesAsync();

                return deletedResponse;
            }

            /*                var entity = _mapper.Map<StudentEntity>(command.Student);

                            // Check if the mapped entity is null
                            if (entity == null)
                            {
                                return null;
                            }

                            _dbContext.Entry(entity).State = EntityState.Modified;

                            await _dbContext.SaveChangesAsync(cancellationToken);

                            // Map the updated entity back to SaveStudentResponse
                            var updatedResponse = _mapper.Map<SaveStudentResponse>(entity);

                            return updatedResponse;*/
        }

    }

    #endregion
}

using AutoMapper;
using MediatR;
using SchoolManagementSystem.DAL;
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Portal.Shared.Request;
using SchoolManagementSystem.Portal.Shared.Response;
using SchoolManagementSystem.Application;

namespace SchoolManagementSystem.Core.Api.cqrs.Commands.StudentCommands;

public static class StudentCommands
{
    #region SaveStudent
    public class SaveStudentCommand : IntegrationEvent, IRequest<SaveStudentResponse>
    {
        public SaveStudentCommand(SaveStudentRequest student) => Student = student;
        public SaveStudentRequest Student { get; set; }
       // public Guid EventId { get ; set ; }
    }

    public sealed class SaveStudentCommandHandler : IRequestHandler<SaveStudentCommand, SaveStudentResponse>
    {
        //private readonly ApplicationDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SaveStudentCommandHandler(IServiceProvider serviceProvider)
        {
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        }

        public async Task<SaveStudentResponse> Handle(SaveStudentCommand command, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<StudentEntity>(command.Student);

            try
            {
                //entity.DateOfBirth = new DateTime(long.Parse(entity.DateOfBirth.ToString("yyyy-MM-dd")));

                await _unitOfWork.Students.AddAsync(entity);

                await _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                var ss = ex.Message;
            }
            var persisted = await _unitOfWork.Students.GetByIdAsync(entity.Id);
            return _mapper.Map<SaveStudentResponse>(persisted);
           

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
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public UpdateStudentCommandHandler(IServiceProvider serviceProvider)
            {
                _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
                _mapper = serviceProvider.GetRequiredService<IMapper>();
            }

            public async Task<SaveStudentResponse> Handle(UpdateStudentCommand command, CancellationToken cancellationToken)
            {
                var entity = _mapper.Map<StudentEntity>(command.Student);
               
                try
                {
                    await _unitOfWork.Students.UpdateAsync(entity);
                    await _unitOfWork.Commit();

                }
                catch (Exception ex)
                {
                    var tt = ex.Message;
                }
                return _mapper.Map<SaveStudentResponse>(entity);

            }
        }
    }

    #endregion

    #region  DeleteStudent
    public class DeleteStudentCommand : IRequest<SaveStudentResponse>
    {
        public DeleteStudentCommand(Guid id)
        {
            StudentId = id;
        }
        public Guid StudentId { get; set; }

        public sealed class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, SaveStudentResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public DeleteStudentCommandHandler(IServiceProvider serviceProvider)
            {
                _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
                _mapper = serviceProvider.GetRequiredService<IMapper>();
            }

            public async Task<SaveStudentResponse> Handle(DeleteStudentCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var entity = await _unitOfWork.Students.GetByIdAsync(command.StudentId);
                    await _unitOfWork.Students.RemoveAsync(entity);
                    _unitOfWork.Commit();

                    return _mapper.Map<SaveStudentResponse>(entity);
                }
                catch(NullReferenceException ex) 
                {
                    throw new NullReferenceException($"Student not Found {ex.Message}");
                }
                 
            }

        }

    }

    #endregion
}

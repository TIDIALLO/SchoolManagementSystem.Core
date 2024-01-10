using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.DAL;
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Portal.Shared.Request;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagementSystem.Portal.Shared.Response;

namespace SchoolManagementSystem.Core.Api.cqrs.Commands.EnrollmentCommands;

public static class EnrollmentCommands
{
 #region SaveEnrollment
    public class SaveEnrollmentCommand : IRequest<SaveEnrollmentResponse>
    {
#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
#pragma warning disable CS1717 // Assignation effectuée à la même variable
        public SaveEnrollmentCommand(SaveEnrollmentRequest Enrollment) => Enrollment = Enrollment;
#pragma warning restore CS1717 // Assignation effectuée à la même variable
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        public SaveEnrollmentRequest Enrollment { get; set; }
    }

    public sealed class SaveEnrollmentCommandHandler : IRequestHandler<SaveEnrollmentCommand, SaveEnrollmentResponse>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public SaveEnrollmentCommandHandler(IServiceProvider serviceProvider)
        {
            _dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _logger = serviceProvider.GetRequiredService<ILogger>();   
        }

        public async Task<SaveEnrollmentResponse> Handle(SaveEnrollmentCommand command, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<EnrollmentEntity>(command.Enrollment);

            await _dbContext.Enrollments.AddAsync(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var persited = await _dbContext.Enrollments.FirstOrDefaultAsync(s => s.Id == entity.Id);

            return _mapper.Map<SaveEnrollmentResponse>(persited);
        }
    }

    #endregion

    #region  UpdateEnrollment
    public class UpdateEnrollmentCommand : IRequest<SaveEnrollmentResponse>
    {
        public UpdateEnrollmentCommand(SaveEnrollmentRequest enrollment)
        {
            Enrollment = enrollment;
        }

        public SaveEnrollmentRequest Enrollment { get; set; }

        public sealed class UpdateEnrollmentCommandHandler : IRequestHandler<UpdateEnrollmentCommand, SaveEnrollmentResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public UpdateEnrollmentCommandHandler(IServiceProvider serviceProvider)
            {
                _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
                _mapper = serviceProvider.GetRequiredService<IMapper>();
            }

            public async Task<SaveEnrollmentResponse> Handle(UpdateEnrollmentCommand command, CancellationToken cancellationToken)
            {
                var entity = _mapper.Map<EnrollmentEntity>(command.Enrollment);
                await _unitOfWork.Enrollments.UpdateAsync(entity);
                await _unitOfWork.Commit();

                return _mapper.Map<SaveEnrollmentResponse>(entity); ;
            }
        }
    }
    #endregion

    #region  DeleteEnrollment
    public class DeleteEnrollmentCommand : IRequest<SaveEnrollmentResponse>
    {
        public DeleteEnrollmentCommand(Guid id)
        {
            EnrollmentId = id;
        }
        public Guid EnrollmentId { get; set; }

        public sealed class DeleteEnrollmentCommandHandler : IRequestHandler<DeleteEnrollmentCommand, SaveEnrollmentResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly ILogger _logger;

            public DeleteEnrollmentCommandHandler(IServiceProvider serviceProvider)
            {
                _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
                _mapper = serviceProvider.GetRequiredService<IMapper>();
                _logger = serviceProvider.GetRequiredService<ILogger>();

            }

            public async Task<SaveEnrollmentResponse> Handle(DeleteEnrollmentCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var entity = await _unitOfWork.Enrollments.GetByIdAsync(command.EnrollmentId);
                    await _unitOfWork.Enrollments.RemoveAsync(entity);
                    await _unitOfWork.Commit();

                    return _mapper.Map<SaveEnrollmentResponse>(entity);
                }
                catch (NullReferenceException ex)
                {
                    _logger.LogInformation($"Enrollment not Found {ex.Message}");
                    throw new NullReferenceException($"Enrollment not Found {ex.Message}");
                }

            }

        }

    }
    #endregion
}

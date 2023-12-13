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
        public SaveEnrollmentCommand(SaveEnrollmentRequest Enrollment) => Enrollment = Enrollment;
        public SaveEnrollmentRequest Enrollment { get; set; }
    }

    public sealed class SaveEnrollmentCommandHandler : IRequestHandler<SaveEnrollmentCommand, SaveEnrollmentResponse>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public SaveEnrollmentCommandHandler(IServiceProvider serviceProvider)
        {
            _dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
        }

        public async Task<SaveEnrollmentResponse> Handle(SaveEnrollmentCommand command, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<EnrollmentEntity>(command.Enrollment);

            await _dbContext.Enrollments.AddAsync(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var persited = await _dbContext.Enrollments.FirstOrDefaultAsync(s => s.EnrollmentId == entity.EnrollmentId);

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
            private readonly ApplicationDbContext _dbContext;
            private readonly IMapper _mapper;

            public UpdateEnrollmentCommandHandler(IServiceProvider serviceProvider)
            {
                _dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
                _mapper = serviceProvider.GetRequiredService<IMapper>();
            }

            public async Task<SaveEnrollmentResponse> Handle(UpdateEnrollmentCommand command, CancellationToken cancellationToken)
            {
                var entity = _mapper.Map<EnrollmentEntity>(command.Enrollment);

                // Check if the mapped entity is null
                if (entity == null)
                {
                    return null;
                }

                _dbContext.Entry(entity).State = EntityState.Modified;

                await _dbContext.SaveChangesAsync(cancellationToken);

                // Map the updated entity back to SaveEnrollmentResponse
                var updatedResponse = _mapper.Map<SaveEnrollmentResponse>(entity);

                return updatedResponse;
            }
        }
    }

    #endregion

    #region  DeleteEnrollment
    public class DeleteEnrollmentCommand : IRequest<SaveEnrollmentResponse>
    {
        public DeleteEnrollmentCommand(Guid enrollmentId)
        {
            EnrollmentId = enrollmentId;
        }
        public Guid EnrollmentId { get; }

        //public SaveEnrollmentRequest Enrollment { get; set; }

        public sealed class DeleteEnrollmentCommandHandler : IRequestHandler<DeleteEnrollmentCommand, SaveEnrollmentResponse>
        {
            private readonly ApplicationDbContext _dbContext;
            private readonly IMapper _mapper;

            public DeleteEnrollmentCommandHandler(IServiceProvider serviceProvider)
            {
                _dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
                _mapper = serviceProvider.GetRequiredService<IMapper>();
            }

            public async Task<SaveEnrollmentResponse> Handle(DeleteEnrollmentCommand command, CancellationToken cancellationToken)
            {
                
                var enrollmentToRemove = await _dbContext.Enrollments.FindAsync(command.EnrollmentId);

                if (enrollmentToRemove == null)
                {
                    return null;
                }

                var deletedResponse = _mapper.Map<SaveEnrollmentResponse>(enrollmentToRemove);

                _dbContext.Enrollments.Remove(enrollmentToRemove);
                await _dbContext.SaveChangesAsync();

                return deletedResponse;
            }

            /*                var entity = _mapper.Map<EnrollmentEntity>(command.Enrollment);

                            // Check if the mapped entity is null
                            if (entity == null)
                            {
                                return null;
                            }

                            _dbContext.Entry(entity).State = EntityState.Modified;

                            await _dbContext.SaveChangesAsync(cancellationToken);

                            // Map the updated entity back to SaveEnrollmentResponse
                            var updatedResponse = _mapper.Map<SaveEnrollmentResponse>(entity);

                            return updatedResponse;*/
        }

    }

    #endregion
}

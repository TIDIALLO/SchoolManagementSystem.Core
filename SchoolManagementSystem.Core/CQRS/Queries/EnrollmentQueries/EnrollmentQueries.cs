using AutoMapper;
using SchoolManagementSystem.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Portal.Shared.Response;
using SchoolManagementSystem.Portal.Shared.Request;

namespace SchoolManagementSystem.Core.Api.cqrs.Queries.EnrollmentQueries;

public static class EnrollmentQueries
{
    /// <summary>
    /// GetEnrollment
    /// </summary>
    #region GetStudent
    public class GetEnrollmentQuery : IRequest<SaveEnrollmentResponse>
    {
        public GetEnrollmentQuery(Guid EnrollmentId)
        {
            EnrollmentId = EnrollmentId;
        }

        public Guid EnrollmentId { get; set; }
    }

    public class GetEnrollmentQueryHandler : IRequestHandler<GetEnrollmentQuery, SaveEnrollmentResponse>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetEnrollmentQueryHandler(IServiceProvider serviceProvider)
        {
            _dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
        }

        public async Task<SaveEnrollmentResponse> Handle(GetEnrollmentQuery query, CancellationToken cancellationToken)
        {
            var persisted = await _dbContext.Enrollments.FirstOrDefaultAsync(e => e.EnrollmentId == query.EnrollmentId, cancellationToken);
            return persisted == null ? null : _mapper.Map<SaveEnrollmentResponse>(persisted);
        }
    }
    #endregion

    /// <summary>
    /// GetAllEnrollments
    /// </summary>
    #region GetAllEnrollments
    public class GetAllEnrollmentQuery : IRequest<List<SaveEnrollmentResponse>>
    {
        public GetAllEnrollmentQuery()
        {
        }
        public List<SaveEnrollmentResponse> Enrollment { get; set; }
    }

    public class GetAllEnrollmentQueryHandler : IRequestHandler<GetAllEnrollmentQuery, List<SaveEnrollmentResponse>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllEnrollmentQueryHandler(IServiceProvider serviceProvider)
        {
            _dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            _mapper = serviceProvider.GetRequiredService<IMapper> ();
        }

        public async Task<List<SaveEnrollmentResponse>> Handle(GetAllEnrollmentQuery query, CancellationToken cancellationToken)
        {
            var Enrollments = await _dbContext.Enrollments.ToListAsync(cancellationToken);
            return _mapper.Map<List<SaveEnrollmentResponse>>(Enrollments);
        }
    }

    #endregion


}

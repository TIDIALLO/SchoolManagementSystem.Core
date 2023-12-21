using AutoMapper;
using SchoolManagementSystem.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Portal.Shared.Response;
using SchoolManagementSystem.Portal.Shared.Request;
using SchoolManagementSystem.Application.Interfaces;
using Hangfire;
using Workers;

namespace SchoolManagementSystem.Core.Api.cqrs.Queries.StudentQueries;

public static class CourseQueries
{
    /// <summary>
    /// GetStudent
    /// </summary>
    #region GetStudent
    public class GetStudentQuery : IRequest<SaveStudentResponse> , ICachable<SaveStudentResponse>
    {
        public GetStudentQuery(Guid studentId)
        {
            StudentId = studentId;
            Key = studentId.ToString();
        }

        public Guid StudentId { get; set; }
        public string Key { get; set; }
        public int Expiration { get; set; } = 30;
    }

    public class GetStudentQueryHandler : IRequestHandler<GetStudentQuery, SaveStudentResponse>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetStudentQueryHandler(IServiceProvider serviceProvider)
        {
            _dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
        }

        public async Task<SaveStudentResponse?> Handle(GetStudentQuery query, CancellationToken cancellationToken)
        {
            var persisted = await _dbContext.Students.FirstOrDefaultAsync(e => e.Id == query.StudentId, cancellationToken);
            return persisted == null ? null : _mapper.Map<SaveStudentResponse>(persisted);
        }
    }
    #endregion
    
    /// <summary>
    /// GetAllStudents
    /// </summary>
    #region GetAllStudents
    public class GetAllStudentQuery : IRequest<List<SaveStudentResponse>>, ICachable<List<SaveStudentResponse>>
    {
        public GetAllStudentQuery()
        {
        }
        public List<SaveStudentResponse> Student { get; set; }
        public string Key { get; set; } = "AllStudents";
        public int Expiration { get; set; } = 5;
    }

    public class GetAllStudentQueryHandler : IRequestHandler<GetAllStudentQuery, List<SaveStudentResponse>>
    {
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IEmailWorker _emailWorker;


        public GetAllStudentQueryHandler(IServiceProvider serviceProvider)
        {
            _dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            _mapper = serviceProvider.GetRequiredService<IMapper> ();
            _backgroundJobClient = serviceProvider.GetRequiredService<IBackgroundJobClient>();
            _emailWorker = serviceProvider.GetRequiredService<IEmailWorker>();
        }

        public async Task<List<SaveStudentResponse>> Handle(GetAllStudentQuery query, CancellationToken cancellationToken)
        {
            var students = await _dbContext.Students.ToListAsync(cancellationToken);
            //Background Job.
            _backgroundJobClient.Enqueue(() => _emailWorker.SendEmail("Email Batch", "######", "Welcome to the website."));
            _backgroundJobClient.Schedule(() => _emailWorker.SendEmail("Schedule", "*********", "Welcome to the website."), TimeSpan.FromSeconds(10));

            return _mapper.Map<List<SaveStudentResponse>>(students);
        }
    }

    #endregion


}

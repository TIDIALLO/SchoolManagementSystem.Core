using AutoMapper;
using SchoolManagementSystem.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Portal.Shared.Response;
using SchoolManagementSystem.Portal.Shared.Request;
using SchoolManagementSystem.Application.Interfaces;
using Hangfire;
using Workers;
using SchoolManagementSystem.Proxy;
using SchoolManagementSystem.Domain.Models;
using SchoolManagementSystem.Application;

namespace SchoolManagementSystem.Core.Api.cqrs.Queries.StudentQueries;

public static class StudentQueries
{
    /// <summary>
    /// GetStudent
    /// </summary>
    #region GetStudent
    public class GetStudentQuery : IntegrationEvent, IRequest<SaveStudentRequest> , ICachable<SaveStudentRequest>
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

    public class GetStudentQueryHandler : IRequestHandler<GetStudentQuery, SaveStudentRequest>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetStudentQueryHandler(IServiceProvider serviceProvider)
        {
            _dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
        }

        public async Task<SaveStudentRequest?> Handle(GetStudentQuery query, CancellationToken cancellationToken)
        {
            var persisted = await _dbContext.Students.FirstOrDefaultAsync(e => e.Id == query.StudentId, cancellationToken);
            return persisted == null ? null : _mapper.Map<SaveStudentRequest>(persisted);
        }
    }
    #endregion
    
    /// <summary>
    /// GetAllStudents
    /// </summary>
    #region GetAllStudents
    public class GetAllStudentQuery : IntegrationEvent, IRequest<List<SaveStudentResponse>>, ICachable<List<SaveStudentResponse>>
    {
        public GetAllStudentQuery()
        {
        }

        public GetAllStudentQuery(Guid eventId) : base(eventId)
        {
        }


        public List<SaveStudentResponse> Student { get; set; }
        public string Key { get; set; } = "AllStudents";
        public int Expiration { get; set; } = 5;
    }

    public class GetAllStudentQueryHandler : IRequestHandler<GetAllStudentQuery, List<SaveStudentResponse>>
    {
        private readonly ILogger<GetAllStudentQueryHandler> _logger;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IEmailWorker _emailWorker;
        private readonly IProxy _proxy;
        private readonly IConfiguration _configuration;


        public GetAllStudentQueryHandler(IServiceProvider serviceProvider)
        {
            _dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            _mapper = serviceProvider.GetRequiredService<IMapper> ();
            _backgroundJobClient = serviceProvider.GetRequiredService<IBackgroundJobClient>();
            _emailWorker = serviceProvider.GetRequiredService<IEmailWorker>();
             _proxy = serviceProvider.GetRequiredService<IProxy>();
            _configuration = serviceProvider.GetRequiredService<IConfiguration>();
            _logger = serviceProvider.GetRequiredService<ILogger<GetAllStudentQueryHandler>>();
        }

        public async Task<List<SaveStudentResponse>> Handle(GetAllStudentQuery query, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Students.ToListAsync(cancellationToken);

           /* var requestCommand = new RequestCommand
            {
                Uri = $"{_configuration["NewletterUri"]}WeatherForecast/notification-count",
            };
            var content = await _proxy.Get<NotificationModel>(requestCommand);
            _logger.LogInformation($"----- result call to {requestCommand.Uri}:{content.Data}");


            //Background Job.
            _backgroundJobClient.Enqueue(() => _emailWorker.SendEmail("Email ", "######", "Welcome to the website."));
            _backgroundJobClient.Schedule(() => _emailWorker.SendNewsletter("Newsletter", "******** Newsletter 1 *******"), TimeSpan.FromSeconds(5));
*/
            return result == null ? null : _mapper.Map<List<SaveStudentResponse>>(result);

            //return _mapper.Map<List<SaveStudentResponse>>(students);
        }
    }

    #endregion


}

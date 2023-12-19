using AutoMapper;
using SchoolManagementSystem.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Portal.Shared.Response;
using SchoolManagementSystem.Portal.Shared.Request;

namespace SchoolManagementSystem.Core.Api.cqrs.Queries.CourseQueries;

public static class CourseQueries
{
    /// <summary>
    /// GetCourse
    /// </summary>
    #region GetCourse
    public class GetCourseQuery : IRequest<SaveCourseResponse>
    {
        public GetCourseQuery(Guid CourseId)
        {
            CourseId = CourseId;
        }

        public Guid CourseId { get; set; }
    }

    public class GetCourseQueryHandler : IRequestHandler<GetCourseQuery, SaveCourseResponse>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetCourseQueryHandler(IServiceProvider serviceProvider)
        {
            _dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
        }

        public async Task<SaveCourseResponse> Handle(GetCourseQuery query, CancellationToken cancellationToken)
        {
            var persisted = await _dbContext.Courses.FirstOrDefaultAsync(e => e.Id == query.CourseId, cancellationToken);
            return persisted == null ? null : _mapper.Map<SaveCourseResponse>(persisted);
        }
    }
    #endregion
    
    /// <summary>
    /// GetAllCourses
    /// </summary>
    #region GetAllCourses
    public class GetAllCourseQuery : IRequest<List<SaveCourseResponse>>
    {
        public GetAllCourseQuery()
        {
        }
        public List<SaveCourseResponse> Course { get; set; }
    }

    public class GetAllCourseQueryHandler : IRequestHandler<GetAllCourseQuery, List<SaveCourseResponse>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllCourseQueryHandler(IServiceProvider serviceProvider)
        {
            _dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            _mapper = serviceProvider.GetRequiredService<IMapper> ();
        }

        public async Task<List<SaveCourseResponse>> Handle(GetAllCourseQuery query, CancellationToken cancellationToken)
        {
            MemoryStream memoryStream = null;
            var position = memoryStream.Position;

            var courses = await _dbContext.Courses.ToListAsync(cancellationToken);
            return  _mapper.Map<List<SaveCourseResponse>>(courses);
        }
    }

    #endregion


}

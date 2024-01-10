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
        public GetCourseQuery(Guid id)
        {
            CourseId = id;
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
#pragma warning disable CS8603 // Existence possible d'un retour de référence null.
            return persisted == null ? null : _mapper.Map<SaveCourseResponse>(persisted);
#pragma warning restore CS8603 // Existence possible d'un retour de référence null.
        }
    }
    #endregion
    
    /// <summary>
    /// GetAllCourses
    /// </summary>
    #region GetAllCourses
    public class GetAllCourseQuery : IRequest<List<SaveCourseResponse>>
    {
#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        public GetAllCourseQuery()
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        {
        }
        public List<SaveCourseResponse> Course { get; set; }
    }

    public class GetAllCourseQueryHandler : IRequestHandler<GetAllCourseQuery, List<SaveCourseResponse>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCourseQueryHandler(IServiceProvider serviceProvider)
        {
            _dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        }

        public async Task<List<SaveCourseResponse>> Handle(GetAllCourseQuery query, CancellationToken cancellationToken)
        {
            // MemoryStream memoryStream = null;
            //var position = memoryStream.Position;

            //var courses = await _dbContext.Courses.ToListAsync(cancellationToken);
            var result = await _unitOfWork.Courses.GetAll();
            return  _mapper.Map<List<SaveCourseResponse>>(result);
        }
    }

    #endregion


}

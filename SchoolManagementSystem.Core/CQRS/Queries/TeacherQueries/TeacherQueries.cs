using AutoMapper;
using SchoolManagementSystem.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Portal.Shared.Response;
using SchoolManagementSystem.Portal.Shared.Request;

namespace SchoolManagementSystem.Core.Api.cqrs.Queries.TeacherQueries;

public static class TeacherQueries
{
    /// <summary>
    /// GetTeacher
    /// </summary>
    #region GetTeacher
    public class GetTeacherQuery : IRequest<SaveTeacherRequest>
    {
        public GetTeacherQuery(Guid teacherId)
        {
            TeacherId = teacherId;
        }

        public Guid TeacherId { get; set; }
    }

    public class GetTeacherQueryHandler : IRequestHandler<GetTeacherQuery, SaveTeacherRequest>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetTeacherQueryHandler(IServiceProvider serviceProvider)
        {
            _dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
        }

        public async Task<SaveTeacherRequest> Handle(GetTeacherQuery query, CancellationToken cancellationToken)
        {
            var persisted = await _dbContext.Teacher.FirstOrDefaultAsync(e => e.Id == query.TeacherId, cancellationToken);
#pragma warning disable CS8603 // Existence possible d'un retour de référence null.
            return persisted == null ? null : _mapper.Map<SaveTeacherRequest>(persisted);
#pragma warning restore CS8603 // Existence possible d'un retour de référence null.
        }
    }
    #endregion
    
    /// <summary>
    /// GetAllTeachers
    /// </summary>
    #region GetAllTeachers
    public class GetAllTeacherQuery : IRequest<List<SaveTeacherResponse>>
    {
#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        public GetAllTeacherQuery()
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        {
        }
        public List<SaveTeacherResponse> Teacher { get; set; }
    }

    public class GetAllTeacherQueryHandler : IRequestHandler<GetAllTeacherQuery, List<SaveTeacherResponse>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        //private readonly IUnitOfWork _unitOfWork;


        public GetAllTeacherQueryHandler(IServiceProvider serviceProvider)
        {
            _dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            _mapper = serviceProvider.GetRequiredService<IMapper> ();
            //_unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();

        }

        public async Task<List<SaveTeacherResponse>> Handle(GetAllTeacherQuery query, CancellationToken cancellationToken)
        {
            var teachers = await _dbContext.Teacher.ToListAsync(cancellationToken);
            return _mapper.Map<List<SaveTeacherResponse>>(teachers);
     
        }
    }

    #endregion

}

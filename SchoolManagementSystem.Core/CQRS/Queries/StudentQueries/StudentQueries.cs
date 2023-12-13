﻿using AutoMapper;
using SchoolManagementSystem.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Portal.Shared.Response;
using SchoolManagementSystem.Portal.Shared.Request;

namespace SchoolManagementSystem.Core.Api.cqrs.Queries.StudentQueries;

public static class CourseQueries
{
    /// <summary>
    /// GetStudent
    /// </summary>
    #region GetStudent
    public class GetStudentQuery : IRequest<SaveStudentResponse>
    {
        public GetStudentQuery(Guid studentId)
        {
            StudentId = studentId;
        }

        public Guid StudentId { get; set; }
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

        public async Task<SaveStudentResponse> Handle(GetStudentQuery query, CancellationToken cancellationToken)
        {
            var persisted = await _dbContext.Students.FirstOrDefaultAsync(e => e.StudentId == query.StudentId, cancellationToken);
            return persisted == null ? null : _mapper.Map<SaveStudentResponse>(persisted);
        }
    }
    #endregion
    
    /// <summary>
    /// GetAllStudents
    /// </summary>
    #region GetAllStudents
    public class GetAllStudentQuery : IRequest<List<SaveStudentResponse>>
    {
        public GetAllStudentQuery()
        {
        }
        public List<SaveStudentResponse> Student { get; set; }
    }

    public class GetAllStudentQueryHandler : IRequestHandler<GetAllStudentQuery, List<SaveStudentResponse>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllStudentQueryHandler(IServiceProvider serviceProvider)
        {
            _dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            _mapper = serviceProvider.GetRequiredService<IMapper> ();
        }

        public async Task<List<SaveStudentResponse>> Handle(GetAllStudentQuery query, CancellationToken cancellationToken)
        {
            var students = await _dbContext.Students.ToListAsync(cancellationToken);
            return _mapper.Map<List<SaveStudentResponse>>(students);
        }
    }

    #endregion


}

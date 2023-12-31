﻿using AutoMapper;
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
    public class GetTeacherQuery : IRequest<SaveTeacherResponse>
    {
        public GetTeacherQuery(Guid TeacherId)
        {
            TeacherId = TeacherId;
        }

        public Guid TeacherId { get; set; }
    }

    public class GetTeacherQueryHandler : IRequestHandler<GetTeacherQuery, SaveTeacherResponse>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetTeacherQueryHandler(IServiceProvider serviceProvider)
        {
            _dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
        }

        public async Task<SaveTeacherResponse> Handle(GetTeacherQuery query, CancellationToken cancellationToken)
        {
            var persisted = await _dbContext.Teacher.FirstOrDefaultAsync(e => e.Id == query.TeacherId, cancellationToken);
            return persisted == null ? null : _mapper.Map<SaveTeacherResponse>(persisted);
        }
    }
    #endregion
    
    /// <summary>
    /// GetAllTeachers
    /// </summary>
    #region GetAllTeachers
    public class GetAllTeacherQuery : IRequest<List<SaveTeacherResponse>>
    {
        public GetAllTeacherQuery()
        {
        }
        public List<SaveTeacherResponse> Teacher { get; set; }
    }

    public class GetAllTeacherQueryHandler : IRequestHandler<GetAllTeacherQuery, List<SaveTeacherResponse>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllTeacherQueryHandler(IServiceProvider serviceProvider)
        {
            _dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            _mapper = serviceProvider.GetRequiredService<IMapper> ();
        }

        public async Task<List<SaveTeacherResponse>> Handle(GetAllTeacherQuery query, CancellationToken cancellationToken)
        {
            var teachers = await _dbContext.Teacher.ToListAsync(cancellationToken);
            return _mapper.Map<List<SaveTeacherResponse>>(teachers);
        }
    }

    #endregion

}

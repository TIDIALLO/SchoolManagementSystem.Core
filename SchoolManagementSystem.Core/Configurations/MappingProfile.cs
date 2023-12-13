using AutoMapper;
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Portal.Shared.Request;
using SchoolManagementSystem.Portal.Shared.Response;

namespace SchoolManagementSystem.Core.Api.Configurations;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<SaveStudentRequest, StudentEntity>().ReverseMap();
        CreateMap<StudentEntity, SaveStudentResponse>().ReverseMap();

        CreateMap<SaveTeacherRequest, TeacherEntity>().ReverseMap();
        CreateMap<TeacherEntity, SaveTeacherResponse>().ReverseMap();

        CreateMap<SaveCourseRequest, CourseEntity>().ReverseMap();
        CreateMap<CourseEntity, SaveCourseResponse>().ReverseMap();

        CreateMap<SaveEnrollmentRequest, EnrollmentEntity>().ReverseMap();
        CreateMap<EnrollmentEntity, SaveEnrollmentResponse>().ReverseMap();
    }
}

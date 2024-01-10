using AutoMapper;
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Portal.Shared.Mocks.Request;
using SchoolManagementSystem.Portal.Shared.Request;
using SchoolManagementSystem.Portal.Shared.Response;
namespace SchoolManagementSystem.Core.Api.Configurations;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<SaveStudentRequest, StudentEntity>()
            //.ForMember(e => e.Id, options => options.MapFrom(e => e.StudentId))
            .ReverseMap();
        CreateMap<StudentEntity, SaveStudentResponse>()
            .ForMember(e => e.StudentId, options => options.MapFrom(e => e.Id)).ReverseMap();


        CreateMap<SaveTeacherRequest, TeacherEntity>().ReverseMap();
        CreateMap<TeacherEntity, SaveTeacherResponse>().ReverseMap();

        CreateMap<SaveCourseRequest, CourseEntity>().ReverseMap();
        CreateMap<CourseEntity, SaveCourseResponse>().ReverseMap();

        /*   CreateMap<SaveCourseRequest, CourseEntity>()
        .ReverseMap();

           CreateMap<CourseEntity, SaveCourseResponse>()
               .ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description)) // Make sure the property names match
               .ReverseMap();*/


        CreateMap<SaveEnrollmentRequest, EnrollmentEntity>().ReverseMap();
        CreateMap<EnrollmentEntity, SaveEnrollmentResponse>().ReverseMap();
    }
}

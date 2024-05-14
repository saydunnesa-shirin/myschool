using Api.Features.Enrollments;
using AutoMapper;

namespace Api.Domain.Enrollments;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        GetList();
    }

    private void GetList()
    {
        CreateMap<Enrollment, EnrollmentResult>(MemberList.None)
        .ForMember(dest => dest.AcademicSessionName, opt => opt.MapFrom((src, dest) => src.AcademicSession.Name))
        .ForMember(dest => dest.AcademicClassName, opt => opt.MapFrom((src, dest) => src.AcademicClass.Name))
        .ForMember(dest => dest.StudentName, opt => opt.MapFrom((src, dest) => src.Student.FirstName + ' ' + src.Student.LastName))
        .ForMember(dest => dest.StudentIdNumber, opt => opt.MapFrom((src, dest) => src.Student.StudentId))
        .ForMember(dest => dest.StatusId, opt => opt.MapFrom((src, dest) => src.Student.StatusId))
        .ForMember(dest => dest.StatusReasonId, opt => opt.MapFrom((src, dest) => src.Student.StatusReasonId));
    }
}

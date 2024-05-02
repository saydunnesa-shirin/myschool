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
        .ForMember(dest => dest.AcademicClassName, opt => opt.MapFrom((src, dest) => src.AcademicSession.Name));
    }
}

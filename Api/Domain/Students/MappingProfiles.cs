using Api.Common;
using Api.Features.Students;
using AutoMapper;

namespace Api.Domain.Students;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        GetList();
    }

    private void GetList()
    {
        CreateMap<Student, StudentResult>(MemberList.None)
        .ForMember(dest => dest.InstitutionName, opt => opt.MapFrom((src, dest) => src.Institution.Name))
        .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Id == null ? "" : src.Country.Name))
        .ForMember(dest => dest.ActiveSessionName, opt => opt.MapFrom((src, dest) => src.AcademicSession.Name))
        .ForMember(dest => dest.ActiveClassName, opt => opt.MapFrom((src, dest) => src.AcademicClass.Name))

        .ForMember(dest => dest.Status, opt => opt.MapFrom((src, dest) => Enum.GetName(typeof(StudentStatus), src.StatusId)))
        .ForMember(dest => dest.StatusReason, opt => opt.MapFrom(src => src.StatusReasonId == null? "" : Enum.GetName(typeof(StudentStatusReason), src.StatusReasonId)));
    }
}

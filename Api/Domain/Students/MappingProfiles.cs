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
            .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Id == null ? "" : src.Country.Name));
    }
}

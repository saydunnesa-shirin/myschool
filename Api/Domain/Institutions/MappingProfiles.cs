using Api.Features.Institutions;
using AutoMapper;

namespace Api.Domain.Institutions;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        GetList();
    }

    private void GetList()
    {
        CreateMap<Institution, InstitutionResult>(MemberList.None)
            .ForMember(dest => dest.Name, opt => opt.MapFrom((src, dest) => src.Name))
            .ForMember(dest => dest.CountryName, opt => opt.MapFrom((src, dest) => src.Country.Name));
    }
}

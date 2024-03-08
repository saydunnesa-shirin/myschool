using Api.Features.Employees;
using AutoMapper;

namespace Api.Domain.Employees;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        GetList();
    }

    private void GetList()
    {
        CreateMap<Employee, EmployeeResult>(MemberList.None)
            .ForMember(dest => dest.InstitutionName, opt => opt.MapFrom((src, dest) => src.Institution.Name))
            .ForMember(dest => dest.CountryName, opt => opt.MapFrom((src, dest) => src.Country.Name));
    }
}

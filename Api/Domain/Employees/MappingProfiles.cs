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
        CreateMap<EmployeeViewModel, GetEmployee.Result>();
            //.ForMember(x => x.Gender, o => o.MapFrom(src => Enum.GetName(typeof(Gender), src.GenderId)))
            //.ForMember(x => x.EmployeeType, o => o.MapFrom(src => Enum.GetName(typeof(EmployeeType), src.EmployeeTypeId)))
            //.ForMember(x => x.Designation, o => o.MapFrom(src => Enum.GetName(typeof(Designation), src.DesignationId)))
            //.ForMember(x => x.BloodGroup, o => o.MapFrom(src => Enum.GetName(typeof(BloodGroup), src.BloodGroupId)));
        CreateMap<EmployeeViewModel, GetEmployees.Result>();
        CreateMap<EmployeeViewModel, CreateEmployee.Result>();
        CreateMap<EmployeeViewModel, UpdateEmployee.Result>();
    }
}

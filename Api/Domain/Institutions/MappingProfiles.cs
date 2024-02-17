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
        CreateMap<InstitutionViewModel, GetInstitutions.Result>();
        CreateMap<InstitutionViewModel, CreateInstitution.Result>();
        CreateMap<InstitutionViewModel, UpdateInstitution.Result>();
    }
}

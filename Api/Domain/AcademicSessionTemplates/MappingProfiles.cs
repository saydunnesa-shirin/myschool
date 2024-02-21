using Api.Domain.AcademicSessionTemplates;
using Api.Features.AcademicSessionTemplates;
using AutoMapper;

namespace Api.Domain.SessionTemplate
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            GetList();
        }

        private void GetList()
        {
            CreateMap<AcademicSessionTemplateViewModel, GetAcademicSessionTemplate.Result>();
            CreateMap<AcademicSessionTemplateViewModel, GetAcademicSessionTemplates.Result>();

            CreateMap<AcademicSessionTemplateViewModel, CreateAcademicSessionTemplate.Result>();
            CreateMap<AcademicSessionTemplateViewModel, UpdateAcademicSessionTemplate.Result>();
        }
    }
}

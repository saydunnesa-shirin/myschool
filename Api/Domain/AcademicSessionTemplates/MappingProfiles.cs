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
            CreateMap<AcademicSessionTemplate, AcademicSessionTemplateResult>(MemberList.None)
           .ForMember(dest => dest.InstitutionName, opt => opt.MapFrom((src, dest) => src.Institution.Name));
        }
    }
}

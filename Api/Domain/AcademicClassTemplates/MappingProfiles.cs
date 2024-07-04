using Api.Domain.AcademicClassTemplates;
using Api.Features.AcademicClassTemplates;
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
            CreateMap<AcademicClassTemplate, AcademicClassTemplateResult>(MemberList.None)
           .ForMember(dest => dest.InstitutionName, opt => opt.MapFrom((src, dest) => src.Institution.Name));
        }
    }
}

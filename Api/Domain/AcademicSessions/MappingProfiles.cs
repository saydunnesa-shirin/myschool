using Api.Features.AcademicSessions;
using AutoMapper;

namespace Api.Domain.AcademicSessions
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CommonMapping();
        }

        private void CommonMapping()
        {
            CreateMap<AcademicSession, AcademicSessionResult>(MemberList.None)
            .ForMember(dest => dest.Name, opt => opt.MapFrom((src, dest) => src.Name))
            .ForMember(dest => dest.InstitutionName, opt => opt.MapFrom((src, dest) => src.Institution.Name));
        }
    }
}

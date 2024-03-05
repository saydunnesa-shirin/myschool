using Api.Features.AcademicClasses;
using AutoMapper;

namespace Api.Domain.AcademicClasses
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            GetList();
        }

        private void GetList()
        {
            CreateMap<AcademicClass, AcademicClassResult>(MemberList.None)
            .ForMember(dest => dest.Name, opt => opt.MapFrom((src, dest) => src.Name))
            .ForMember(dest => dest.TeacherId, opt => opt.MapFrom((src, dest) => src.InstitutionId));
        }
    }
}

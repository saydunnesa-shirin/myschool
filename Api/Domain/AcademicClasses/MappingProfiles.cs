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
            CreateMap<AcademicClassViewModel, GetAcademicClass.Result>();
            CreateMap<AcademicClassViewModel, GetAcademicClasses.Result>();
            CreateMap<AcademicClassViewModel, CreateAcademicClass.Result>();
            CreateMap<AcademicClassViewModel, UpdateAcademicClass.Result>();
        }
    }
}

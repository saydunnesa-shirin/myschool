using Api.Features.AcademicSessions;
using AutoMapper;

namespace Api.Domain.AcademicSessions
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            GetList();
        }

        private void GetList()
        {
            CreateMap<AcademicSessionViewModel, GetAcademicSession.Result>();
            CreateMap<AcademicSessionViewModel, GetAcademicSessions.Result>();

            CreateMap<AcademicSessionViewModel, CreateAcademicSession.Result>();
            CreateMap<AcademicSessionViewModel, UpdateAcademicSession.Result>();
        }
    }
}

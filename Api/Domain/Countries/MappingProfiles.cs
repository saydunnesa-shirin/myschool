using Api.Features.Countries;
using AutoMapper;

namespace Api.Domain.Countries
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Country, CreateCountry.Result>();
            CreateMap<Country, GetCountries.Result>();
            CreateMap<Country, UpdateCountry.Result>();
        }
    }
}

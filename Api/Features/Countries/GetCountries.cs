using Api.Domain.Countries;
using AutoMapper;
using MediatR;

namespace Api.Features.Countries;

public class GetCountries
{
    public record Query : IRequest<List<Result>>
    {
        public string Term { get; set; } = string.Empty;
        public bool? IsActive { get; set; }
    }

    public record Result : BaseResult
    {
    }

    public class Handler : IRequestHandler<Query, List<Result>>
    {
        private readonly IMapper _mapper;
        private readonly ICountriesRepository _repository;

        public Handler(
          ICountriesRepository repository,
          IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<Result>> Handle(
          Query query,
          CancellationToken cancellationToken)
        {
            List<Result> mappedList = new();
            var countryQuery = new CountryQuery
            {
                IsActive = query.IsActive
            };
            var list = await _repository.GetListByQueryAsync(countryQuery, cancellationToken);

            foreach (var country in list)
            {
                var mapped = _mapper.Map<Country, Result>(country);
                mappedList.Add(mapped);
            }

            return mappedList;
        }
    }
}

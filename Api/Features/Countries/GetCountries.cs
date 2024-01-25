using Api.Domain.Countries;
using AutoMapper;
using MediatR;

namespace Api.Features.Countries;

public class GetCountries
{
    public record Query : IRequest<List<Result>>
    {
        public string Term { get; set; } = string.Empty;
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
            IEnumerable<Country> list;
            List<Result> mappedList = new();
            if (string.IsNullOrEmpty(query.Term))
            {
                list = await _repository.GetAllAsync(cancellationToken);
            }
            else
            {
                list = await _repository.GetListByQueryAsync(cancellationToken);
            }


            foreach (var country in list)
            {
                var mapped = _mapper.Map<Country, Result>(country);
                mappedList.Add(mapped);
            }

            return mappedList;
        }
    }
}

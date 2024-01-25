using Api.Domain.Countries;
using AutoMapper;
using MediatR;

namespace Api.Features.Countries;

public class CreateCountry
{
    public record Command : IRequest<Result>
    {
        public string Name { get; set; }
        public string Iso2Code { get; set; }
    }

    public record Result : BaseResult
    {
    }

    public class Handler : IRequestHandler<Command, Result>
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

        public async Task<Result> Handle(
          Command command,
          CancellationToken cancellationToken)
        {
            var @new = new Country
            {
                Name = command.Name,
                Iso2Code = command.Iso2Code
            };
            var saved = await _repository.CreateAsync(@new, cancellationToken);
            var mappedEmployee = _mapper.Map<Country, Result>(saved);

            return mappedEmployee;
        }
    }
}

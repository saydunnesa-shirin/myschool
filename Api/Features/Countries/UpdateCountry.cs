using Api.Domain.Countries;
using AutoMapper;
using MediatR;

namespace Api.Features.Countries;

public class UpdateCountry
{
    public record Command : IRequest<Result>
    {
        public int Id { get; set; }
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
            var country = new Country
            {
                Id = command.Id,
                Name = command.Name,
                Iso2Code = command.Iso2Code
            };
            var updated = await _repository.UpdateAsync(country, cancellationToken);
            var mappedEmployee = _mapper.Map<Country, Result>(updated);

            return mappedEmployee;
        }
    }
}

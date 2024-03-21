using Api.Domain.Countries;
using Api.Infrastructure.Exceptions;
using AutoMapper;
using MediatR;

namespace Api.Features.Countries;

public class InActiveCountry
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
            var countryToUpdate = await _repository.GetAsync(command.Id, cancellationToken) ?? throw new NotFoundException("Not found");

            countryToUpdate.IsActive = false;
            countryToUpdate.UpdatedBy = 0;
            countryToUpdate.UpdatedDate = DateTime.UtcNow;

            var updated = await _repository.UpdateAsync(countryToUpdate, cancellationToken);
            var mappedEmployee = _mapper.Map<Country, Result>(updated);

            return mappedEmployee;
        }
    }
}

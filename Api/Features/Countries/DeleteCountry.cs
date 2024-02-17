using AutoMapper;
using MediatR;

namespace Api.Features.Countries;

public class DeleteCountry
{
    public record Command : IRequest<int?>
    {
        public int Id { get; set; }
    }
    public class Handler : IRequestHandler<Command, int?>
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

        public async Task<int?> Handle(
          Command command,
          CancellationToken cancellationToken)
        {
            var deleted = await _repository.DeleteAsync(command.Id, cancellationToken);

            return deleted;
        }
    }
}

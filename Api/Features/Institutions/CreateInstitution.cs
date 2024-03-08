using Api.Domain.Institutions;
using AutoMapper;
using MediatR;

namespace Api.Features.Institutions;

public class CreateInstitution
{
    public record Command : IRequest<InstitutionResult>
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int CountryId { get; set; }
    }

    public class Handler : IRequestHandler<Command, InstitutionResult>
    {
        private readonly IMapper _mapper;
        private readonly IInstitutionsRepository _repository;

        public Handler(
          IInstitutionsRepository repository,
          IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<InstitutionResult> Handle(
          Command command,
          CancellationToken cancellationToken)
        {
            var @new = new Institution
            {
                Name = command.Name,
                CountryId = command.CountryId,
                Address = command.Address
            };

            var saved = await _repository.CreateAsync(@new, cancellationToken);

            var institution = await _repository.GetAsync(saved.Id, cancellationToken);

            var mapped = _mapper.Map<Institution, InstitutionResult>(institution);

            return mapped;
        }
    }
}

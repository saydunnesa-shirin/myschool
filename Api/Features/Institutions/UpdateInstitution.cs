using Api.Domain.Institutions;
using AutoMapper;
using MediatR;

namespace Api.Features.Institutions;

public class UpdateInstitution
{
    public record Command : IRequest<Result>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int CountryId { get; set; }
    }

    public record Result : BaseResult
    {
    }

    public class Handler : IRequestHandler<Command, Result>
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

        public async Task<Result> Handle(
          Command command,
          CancellationToken cancellationToken)
        {
            var @update = new Institution
            {
                Id = command.Id,
                Name = command.Name,
                Address = command.Address,
                CountryId = command.CountryId
            };
            var updated = await _repository.UpdateAsync(@update, cancellationToken);

            var institution = await _repository.GetAsync(updated.Id, cancellationToken);

            var mappedInstitution = _mapper.Map<InstitutionViewModel, Result>(institution);

            return mappedInstitution;
        }
    }
}

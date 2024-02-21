using Api.Domain.AcademicSessions;
using AutoMapper;
using MediatR;

namespace Api.Features.AcademicSessions;

public class UpdateAcademicSession
{
    public record Command : IRequest<Result>
    {
        public int Id { get; set; }
        public int InstitutionId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public record Result : BaseResult
    {
    }

    public class Handler : IRequestHandler<Command, Result>
    {
        private readonly IMapper _mapper;
        private readonly IAcademicSessionsRepository _repository;

        public Handler(
          IAcademicSessionsRepository repository,
          IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(
          Command command,
          CancellationToken cancellationToken)
        {
            var @update = new AcademicSession
            {
                Id = command.Id,
                Name = command.Name,
                InstitutionId = command.InstitutionId,
                Description = command.Description,
                StartDate = command.StartDate,
                EndDate = command.EndDate
            };
            var updated = await _repository.UpdateAsync(@update, cancellationToken);

            var AcademicSession = await _repository.GetAsync(updated.Id, cancellationToken);

            var mappedAcademicSession = _mapper.Map<AcademicSessionViewModel, Result>(AcademicSession);

            return mappedAcademicSession;
        }
    }
}

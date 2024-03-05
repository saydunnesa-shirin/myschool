using Api.Domain.AcademicSessions;
using AutoMapper;
using MediatR;

namespace Api.Features.AcademicSessions;

public class CreateAcademicSession
{
    public record Command : IRequest<AcademicSessionResult>
    {
        public int Id { get; set; }
        public int InstitutionId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class Handler : IRequestHandler<Command, AcademicSessionResult>
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

        public async Task<AcademicSessionResult> Handle(
          Command command,
          CancellationToken cancellationToken)
        {
            var @new = new AcademicSession
            {
                Name = command.Name,
                InstitutionId = command.InstitutionId,
                Description = command.Description,
                StartDate = command.StartDate,
                EndDate = command.EndDate
            };
            var saved = await _repository.CreateAsync(@new, cancellationToken);

            var academicSession = await _repository.GetAsync(saved.Id, cancellationToken);

            var mappedAcademicSession = _mapper.Map<AcademicSession, AcademicSessionResult>(academicSession);

            return mappedAcademicSession;
        }
    }
}

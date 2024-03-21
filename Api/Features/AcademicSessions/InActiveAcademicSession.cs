using Api.Domain.AcademicSessions;
using Api.Infrastructure.Exceptions;
using AutoMapper;
using MediatR;

namespace Api.Features.AcademicSessions;

public class InActiveAcademicSession
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
            var academicSessionToUpdate = await _repository.GetAsync(command.Id, cancellationToken) ?? throw new NotFoundException("Not found");
            academicSessionToUpdate.IsActive = false;
            academicSessionToUpdate.CreatedBy = 0;
            academicSessionToUpdate.CreatedDate = DateTime.UtcNow;

            var updated = await _repository.UpdateAsync(academicSessionToUpdate, cancellationToken);

            var mappedAcademicSession = _mapper.Map<AcademicSession, AcademicSessionResult>(updated);

            return mappedAcademicSession;
        }
    }
}

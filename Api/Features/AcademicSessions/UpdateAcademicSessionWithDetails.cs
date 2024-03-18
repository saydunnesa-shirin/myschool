using Api.Domain.AcademicClasses;
using Api.Domain.AcademicSessions;
using AutoMapper;
using MediatR;

namespace Api.Features.AcademicSessions;

public class UpdateAcademicSessionWithDetails
{
    public record Command : IRequest<AcademicSessionResult>
    {
        public int Id { get; set; }
        public int InstitutionId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        //public bool IsActive { get; set; }
        public List<AcademicClass> AcademicClasses { get; set; }
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
            List<AcademicClass> AcademicClasses = new();
            var academicSession = new AcademicSession
            {
                Id = command.Id,
                Name = command.Name,
                InstitutionId = command.InstitutionId,
                Description = command.Description,
                StartDate = command.StartDate,
                EndDate = command.EndDate,
                //IsActive = command.IsActive
            };

            if(command.AcademicClasses != null)
            {
                foreach (var item in command.AcademicClasses!)
                {
                    var @detail = new AcademicClass
                    {
                        Id = item.Id,
                        Name = item.Name,
                        InstitutionId = item.InstitutionId,
                        TeacherId = item.TeacherId,
                        IsActive = item.IsActive
                    };

                    AcademicClasses.Add(@detail);
                }
            }

            var saved = await _repository.UpdateWithDetailsAsync(academicSession, AcademicClasses, cancellationToken);

            var AcademicSession = await _repository.GetAsync(saved.Id, cancellationToken);

            var mappedAcademicSession = _mapper.Map<AcademicSession, AcademicSessionResult>(AcademicSession);

            return mappedAcademicSession;
        }
    }
}

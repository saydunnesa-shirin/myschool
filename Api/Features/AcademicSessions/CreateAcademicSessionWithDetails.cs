using Api.Domain.AcademicClasses;
using Api.Domain.AcademicSessions;
using AutoMapper;
using MediatR;

namespace Api.Features.AcademicSessions;

public class CreateAcademicSessionWithDetails
{
    public record CommandDetail : IRequest<ResultDetail>
    {
        public int Id { get; set; }
        public int InstitutionId { get; set; }
        public int AcademicSessionId { get; set; }
        public int? TeacherId { get; set; }
        public string Name { get; set; }
    }

    public record Command : IRequest<Result>
    {
        public int Id { get; set; }
        public int InstitutionId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<CommandDetail> AcademicClasses {  get; set; }
    }

    public record Result : BaseResult
    {
    }

    public record ResultDetail : AcademicClasses.BaseResult
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
            List<AcademicClass> AcademicClasses = new();
            var @new = new AcademicSession
            {
                Name = command.Name,
                InstitutionId = command.InstitutionId,
                Description = command.Description,
                StartDate = command.StartDate,
                EndDate = command.EndDate
            };

            foreach (var item in command.AcademicClasses)
            {
                var @detail = new AcademicClass
                {
                    Id = item.Id,
                    Name = item.Name,
                    InstitutionId = item.InstitutionId,
                    TeacherId = item.TeacherId
                };

                AcademicClasses.Add(@detail);
            }

            var saved = await _repository.CreateWithDetailsAsync(@new, AcademicClasses, cancellationToken);

            var AcademicSession = await _repository.GetAsync(saved.Id, cancellationToken);

            var mappedAcademicSession = _mapper.Map<AcademicSessionViewModel, Result>(AcademicSession);

            return mappedAcademicSession;
        }
    }
}

using Api.Domain.AcademicClasses;
using AutoMapper;
using MediatR;

namespace Api.Features.AcademicClasses;

public class UpdateAcademicClass
{
    public record Command : IRequest<AcademicClassResult>
    {
        public int Id { get; set; }
        public int InstitutionId { get; set; }
        public int AcademicSessionId { get; set; }
        public int? TeacherId { get; set; }
        public string Name { get; set; }
        public int? TemplateId { get; set; }
    }


    public class Handler : IRequestHandler<Command, AcademicClassResult>
    {
        private readonly IMapper _mapper;
        private readonly IAcademicClassesRepository _repository;

        public Handler(
          IAcademicClassesRepository repository,
          IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AcademicClassResult> Handle(
          Command command,
          CancellationToken cancellationToken)
        {
            var @update = new AcademicClass
            {
                Id = command.Id,
                Name = command.Name,
                InstitutionId = command.InstitutionId,
                AcademicSessionId = command.AcademicSessionId,
                TeacherId = command.TeacherId,
                TemplateId = command.TemplateId,
            };
            var updated = await _repository.UpdateAsync(@update, cancellationToken);

            var academicClass = await _repository.GetAsync(updated.Id, cancellationToken);

            var mappedAcademicClass = _mapper.Map<AcademicClass, AcademicClassResult>(academicClass);

            return mappedAcademicClass;
        }
    }
}

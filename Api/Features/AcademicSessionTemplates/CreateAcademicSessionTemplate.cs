using Api.Domain.AcademicSessionTemplates;
using AutoMapper;
using MediatR;

namespace Api.Features.AcademicSessionTemplates;

public class CreateAcademicSessionTemplate
{
    public record Command : IRequest<AcademicSessionTemplateResult>
    {
        public string TemplateName { get; set; } = string.Empty;
        public int InstitutionId { get; set; }
    }

    public class Handler : IRequestHandler<Command, AcademicSessionTemplateResult>
    {
        private readonly IMapper _mapper;
        private readonly IAcademicSessionTemplatesRepository _repository;

        public Handler(
          IAcademicSessionTemplatesRepository repository,
          IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AcademicSessionTemplateResult> Handle(
          Command command,
          CancellationToken cancellationToken)
        {
            var @new = new AcademicSessionTemplate
            {
                TemplateName = command.TemplateName,
                InstitutionId = command.InstitutionId,
            };
            var saved = await _repository.CreateAsync(@new, cancellationToken);

            var institution = await _repository.GetAsync(saved.Id, cancellationToken);

            var mappedAcademicSessionTemplate = _mapper.Map<AcademicSessionTemplate, AcademicSessionTemplateResult>(institution);

            return mappedAcademicSessionTemplate;
        }
    }
}

using Api.Domain.AcademicSessionTemplates;
using AutoMapper;
using MediatR;

namespace Api.Features.AcademicSessionTemplates;

public class CreateAcademicSessionTemplate
{
    public record Command : IRequest<Result>
    {
        public string TemplateName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int InstitutionId { get; set; }
    }

    public record Result : BaseResult
    {
    }

    public class Handler : IRequestHandler<Command, Result>
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

        public async Task<Result> Handle(
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

            var mappedAcademicSessionTemplate = _mapper.Map<AcademicSessionTemplateViewModel, Result>(institution);

            return mappedAcademicSessionTemplate;
        }
    }
}

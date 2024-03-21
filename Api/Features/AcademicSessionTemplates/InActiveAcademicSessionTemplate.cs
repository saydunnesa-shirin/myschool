using Api.Domain.AcademicSessionTemplates;
using Api.Infrastructure.Exceptions;
using AutoMapper;
using MediatR;

namespace Api.Features.AcademicSessionTemplates;

public class InActiveAcademicSessionTemplate
{
    public record Command : IRequest<AcademicSessionTemplateResult>
    {
        public int Id { get; set; }
        public string TemplateName { get; set; }
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
            var academicSessionTemplateToUpdate = await _repository.GetAsync(command.Id, cancellationToken) ?? throw new NotFoundException("Not found");

            academicSessionTemplateToUpdate.IsActive = false;
            academicSessionTemplateToUpdate.UpdatedBy = 0;
            academicSessionTemplateToUpdate.UpdatedDate = DateTime.UtcNow;
           
            var updated = await _repository.UpdateAsync(academicSessionTemplateToUpdate, cancellationToken);

            var institution = await _repository.GetAsync(updated.Id, cancellationToken);

            var mappedAcademicSessionTemplate = _mapper.Map<AcademicSessionTemplate, AcademicSessionTemplateResult>(institution);

            return mappedAcademicSessionTemplate;
        }
    }
}

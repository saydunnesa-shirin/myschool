using Api.Domain.AcademicClassTemplates;
using Api.Infrastructure.Exceptions;
using AutoMapper;
using MediatR;

namespace Api.Features.AcademicClassTemplates;

public class InActiveAcademicClassTemplate
{
    public record Command : IRequest<AcademicClassTemplateResult>
    {
        public int Id { get; set; }
        public string TemplateName { get; set; }
        public int InstitutionId { get; set; }
    }

    public class Handler : IRequestHandler<Command, AcademicClassTemplateResult>
    {
        private readonly IMapper _mapper;
        private readonly IAcademicClassTemplatesRepository _repository;

        public Handler(
          IAcademicClassTemplatesRepository repository,
          IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AcademicClassTemplateResult> Handle(
          Command command,
          CancellationToken cancellationToken)
        {
            var academicSessionTemplateToUpdate = await _repository.GetAsync(command.Id, cancellationToken) ?? throw new NotFoundException("Not found");

            academicSessionTemplateToUpdate.IsActive = false;
            academicSessionTemplateToUpdate.UpdatedBy = 0;
            academicSessionTemplateToUpdate.UpdatedDate = DateTime.UtcNow;
           
            var updated = await _repository.UpdateAsync(academicSessionTemplateToUpdate, cancellationToken);

            var institution = await _repository.GetAsync(updated.Id, cancellationToken);

            var mappedAcademicClassTemplate = _mapper.Map<AcademicClassTemplate, AcademicClassTemplateResult>(institution);

            return mappedAcademicClassTemplate;
        }
    }
}

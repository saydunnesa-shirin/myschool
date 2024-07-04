using Api.Domain.AcademicClassTemplates;
using AutoMapper;
using MediatR;

namespace Api.Features.AcademicClassTemplates;

public class CreateAcademicClassTemplate
{
    public record Command : IRequest<AcademicClassTemplateResult>
    {
        public string TemplateName { get; set; } = string.Empty;
        public int InstitutionId { get; set; }
        public int SerialNo { get; set; }
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
            var @new = new AcademicClassTemplate
            {
                TemplateName = command.TemplateName,
                InstitutionId = command.InstitutionId,
                SerialNo = command.SerialNo
            };
            var saved = await _repository.CreateAsync(@new, cancellationToken);

            var institution = await _repository.GetAsync(saved.Id, cancellationToken);

            var mappedAcademicClassTemplate = _mapper.Map<AcademicClassTemplate, AcademicClassTemplateResult>(institution);

            return mappedAcademicClassTemplate;
        }
    }
}

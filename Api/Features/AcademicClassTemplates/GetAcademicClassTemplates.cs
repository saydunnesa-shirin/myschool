using Api.Domain.AcademicClassTemplates;
using AutoMapper;
using MediatR;

namespace Api.Features.AcademicClassTemplates;

public class GetAcademicClassTemplates
{
    public record Query : IRequest<List<AcademicClassTemplateResult>>
    {
        public int? Id { get; set; }
        public int? InstitutionId { get; set; }
        public bool? IsActive { get; set; }
        public string Term { get; set; } = string.Empty;
    }

    public class Handler : IRequestHandler<Query, List<AcademicClassTemplateResult>>
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

        public async Task<List<AcademicClassTemplateResult>> Handle(
          Query query,
          CancellationToken cancellationToken)
        {
            List<AcademicClassTemplateResult> mappedList = new();
            var academicSessionTemplateQuery = new AcademicClassTemplateQuery
            {
                InstitutionId = query.InstitutionId,
                IsActive = query.IsActive
            };
            
            var list = await _repository.GetListByQueryAsync(academicSessionTemplateQuery, cancellationToken);
            foreach (var institution in list)
            {
                var mapped = _mapper.Map<AcademicClassTemplate, AcademicClassTemplateResult>(institution);
                mappedList.Add(mapped);
            }
            return mappedList;
        }
    }
}

using Api.Domain.AcademicSessionTemplates;
using AutoMapper;
using MediatR;

namespace Api.Features.AcademicSessionTemplates;

public class GetAcademicSessionTemplates
{
    public record Query : IRequest<List<AcademicSessionTemplateResult>>
    {
        public int? Id { get; set; }
        public int? InstitutionId { get; set; }

        public string Term { get; set; } = string.Empty;
    }

    public class Handler : IRequestHandler<Query, List<AcademicSessionTemplateResult>>
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

        public async Task<List<AcademicSessionTemplateResult>> Handle(
          Query query,
          CancellationToken cancellationToken)
        {
            IEnumerable<AcademicSessionTemplate> list;
            List<AcademicSessionTemplateResult> mappedList = new();

            if(query.InstitutionId != null)
                list = await _repository.GetListByInstitutionAsync((int)query.InstitutionId, cancellationToken);
            else
                list = await _repository.GetListByQueryAsync(cancellationToken);
            foreach (var institution in list)
            {
                var mapped = _mapper.Map<AcademicSessionTemplate, AcademicSessionTemplateResult>(institution);
                mappedList.Add(mapped);
            }
            return mappedList;
        }
    }
}

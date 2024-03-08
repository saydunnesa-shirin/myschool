using Api.Domain.AcademicSessionTemplates;
using AutoMapper;
using MediatR;

namespace Api.Features.AcademicSessionTemplates;

public class GetAcademicSessionTemplate
{
    public record Query : IRequest<List<AcademicSessionTemplateResult>>
    {
        public int Id { get; set; }
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
            List<AcademicSessionTemplateResult> mappedList = new();

            var institution = await _repository.GetAsync(query.Id, cancellationToken);
            var mapped = _mapper.Map<AcademicSessionTemplate, AcademicSessionTemplateResult>(institution);
            mappedList.Add(mapped);

            return mappedList;
        }
    }
}

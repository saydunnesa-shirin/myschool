using Api.Domain.AcademicClasses;
using AutoMapper;
using MediatR;

namespace Api.Features.AcademicClasses;

public class GetAcademicClass
{
    public record Query : IRequest<List<AcademicClassResult>>
    {
        public int Id { get; set; }
        public string Term { get; set; } = string.Empty;
    }

    public class Handler : IRequestHandler<Query, List<AcademicClassResult>>
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

        public async Task<List<AcademicClassResult>> Handle(
          Query query,
          CancellationToken cancellationToken)
        {
            List<AcademicClassResult> mappedList = new();

            var data = await _repository.GetAsync(query.Id, cancellationToken);
            var mapped = _mapper.Map<AcademicClass, AcademicClassResult>(data);
            mappedList.Add(mapped);

            return mappedList;
        }
    }
}

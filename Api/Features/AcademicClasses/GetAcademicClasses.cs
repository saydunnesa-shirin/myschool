using Api.Domain.AcademicClasses;
using AutoMapper;
using MediatR;

namespace Api.Features.AcademicClasses;

public class GetAcademicClasses
{
    public record Query : IRequest<List<AcademicClassResult>>
    {
        public int? Id { get; set; }
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
            IEnumerable<AcademicClass> list;
            List<AcademicClassResult> mappedList = new();
            
            list = await _repository.GetListByQueryAsync(cancellationToken);
            foreach (var academicClass in list)
            {
                var mapped = _mapper.Map<AcademicClass, AcademicClassResult>(academicClass);
                mappedList.Add(mapped);
            }
            return mappedList;
        }
    }
}

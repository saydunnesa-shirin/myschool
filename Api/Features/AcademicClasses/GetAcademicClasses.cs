using Api.Domain.AcademicClasses;
using AutoMapper;
using MediatR;

namespace Api.Features.AcademicClasses;

public class GetAcademicClasses
{
    public record Query : IRequest<List<Result>>
    {
        public int? Id { get; set; }
        public string Term { get; set; } = string.Empty;
    }

    public record Result : BaseResult
    {
    }

    public class Handler : IRequestHandler<Query, List<Result>>
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

        public async Task<List<Result>> Handle(
          Query query,
          CancellationToken cancellationToken)
        {
            IEnumerable<AcademicClassViewModel> list;
            List<Result> mappedList = new();
            
            list = await _repository.GetListByQueryAsync(cancellationToken);
            foreach (var institution in list)
            {
                var mapped = _mapper.Map<AcademicClassViewModel, Result>(institution);
                mappedList.Add(mapped);
            }
            return mappedList;
        }
    }
}

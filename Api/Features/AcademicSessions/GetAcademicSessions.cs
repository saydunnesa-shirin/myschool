using Api.Domain.AcademicSessions;
using AutoMapper;
using MediatR;

namespace Api.Features.AcademicSessions;

public class GetAcademicSessions
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
        private readonly IAcademicSessionsRepository _repository;

        public Handler(
          IAcademicSessionsRepository repository,
          IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<Result>> Handle(
          Query query,
          CancellationToken cancellationToken)
        {
            IEnumerable<AcademicSessionViewModel> list;
            List<Result> mappedList = new();
            
            list = await _repository.GetListByQueryAsync(cancellationToken);
            foreach (var institution in list)
            {
                var mapped = _mapper.Map<AcademicSessionViewModel, Result>(institution);
                mappedList.Add(mapped);
            }
            return mappedList;
        }
    }
}

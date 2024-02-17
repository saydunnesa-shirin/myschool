using Api.Domain.Institutions;
using AutoMapper;
using MediatR;

namespace Api.Features.Institutions;

public class GetInstitutions
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
        private readonly IInstitutionsRepository _repository;

        public Handler(
          IInstitutionsRepository repository,
          IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<Result>> Handle(
          Query query,
          CancellationToken cancellationToken)
        {
            IEnumerable<InstitutionViewModel> list;
            List<Result> mappedList = new();
            
            list = await _repository.GetListByQueryAsync(cancellationToken);
            foreach (var institution in list)
            {
                var mapped = _mapper.Map<InstitutionViewModel, Result>(institution);
                mappedList.Add(mapped);
            }
            return mappedList;
        }
    }
}

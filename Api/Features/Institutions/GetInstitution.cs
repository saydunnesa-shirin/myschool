using Api.Domain.Institutions;
using AutoMapper;
using MediatR;

namespace Api.Features.Institutions;

public class GetInstitution
{
    public record Query : IRequest<List<Result>>
    {
        public int Id { get; set; }
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
            List<Result> mappedList = new();

            var institution = await _repository.GetAsync(query.Id, cancellationToken);
            var mapped = _mapper.Map<InstitutionViewModel, Result>(institution);
            mappedList.Add(mapped);

            return mappedList;
        }
    }
}

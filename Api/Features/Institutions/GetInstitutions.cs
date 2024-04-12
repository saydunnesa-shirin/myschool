using Api.Domain.Institutions;
using AutoMapper;
using MediatR;

namespace Api.Features.Institutions;

public class GetInstitutions
{
    public record Query : IRequest<List<InstitutionResult>>
    {
        public int? Id { get; set; }
        public string Term { get; set; } = string.Empty;
        public bool? IsActive { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<InstitutionResult>>
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

        public async Task<List<InstitutionResult>> Handle(
          Query query,
          CancellationToken cancellationToken)
        {
            List<InstitutionResult> mappedList = new();

            var institutionQuery = new InstitutionQuery
            {
                IsActive = query.IsActive
            };

            var list = await _repository.GetAllAsync(institutionQuery, cancellationToken);

            foreach (var institution in list)
            {
                var mapped = _mapper.Map<Institution, InstitutionResult>(institution);
                mappedList.Add(mapped);
            }
            return mappedList;
        }
    }
}

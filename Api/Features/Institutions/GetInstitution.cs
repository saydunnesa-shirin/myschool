using Api.Domain.AcademicSessions;
using Api.Domain.Institutions;
using Api.Features.AcademicSessions;
using AutoMapper;
using MediatR;

namespace Api.Features.Institutions;

public class GetInstitution
{
    public record Query : IRequest<InstitutionResult>
    {
        public int Id { get; set; }
        public string Term { get; set; } = string.Empty;
    }

    public class Handler : IRequestHandler<Query, InstitutionResult>
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

        public async Task<InstitutionResult> Handle(
          Query query,
          CancellationToken cancellationToken)
        {
            var academicSession = await _repository.GetAsync(query.Id, cancellationToken);
            var mapped = _mapper.Map<Institution, InstitutionResult>(academicSession);

            return mapped;
        }
    }
}

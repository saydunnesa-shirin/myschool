using Api.Domain.AcademicSessions;
using AutoMapper;
using MediatR;

namespace Api.Features.AcademicSessions;

public class GetAcademicSessions
{
    public record Query : IRequest<List<AcademicSessionResult>>
    {
        public int? Id { get; set; }
        public int? InstitutionId { get; set; }
        public string Term { get; set; } = string.Empty;
    }

    public class Handler : IRequestHandler<Query, List<AcademicSessionResult>>
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

        public async Task<List<AcademicSessionResult>> Handle(
          Query query,
          CancellationToken cancellationToken)
        {
            IEnumerable<AcademicSession> academicSessions;
            List<AcademicSessionResult> mappedList = new();

            if (query.InstitutionId != null)
                academicSessions = await _repository.GetListByInstitutionAsync((int)query.InstitutionId, cancellationToken);
            else
                academicSessions = await _repository.GetListByQueryAsync(cancellationToken);

            foreach (var academicSession in academicSessions)
            {
                var mapped = _mapper.Map<AcademicSession, AcademicSessionResult>(academicSession);
                mappedList.Add(mapped);
            }
            return mappedList;
        }
    }
}

using Api.Domain.AcademicSessions;
using AutoMapper;
using MediatR;

namespace Api.Features.AcademicSessions;

public class GetAcademicSessions
{
    public record Query : IRequest<List<Result>>
    {
        public int? Id { get; set; }
        public int? InstitutionId { get; set; }
        public string Term { get; set; } = string.Empty;
        public bool? IsActive { get; set; }
    }

    public record Result
    {
        public int Id { get; set; }
        public int InstitutionId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string InstitutionName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
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
            IEnumerable<AcademicSession> academicSessions;
            List<Result> mappedList = new();

            var academicSessionQuery = new AcademicSessionQuery
            {
                IsActive = query.IsActive,
                InstitutionId = query.InstitutionId
            };

            academicSessions = await _repository.GetListByQueryAsync(academicSessionQuery, cancellationToken);

            foreach (var academicSession in academicSessions)
            {
                var mapped = _mapper.Map<AcademicSession, Result>(academicSession);
                mappedList.Add(mapped);
            }
            return mappedList;
        }
    }
}

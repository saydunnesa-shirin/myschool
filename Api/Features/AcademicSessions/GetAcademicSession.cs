using Api.Domain.AcademicSessions;
using AutoMapper;
using MediatR;

namespace Api.Features.AcademicSessions;

public class GetAcademicSession
{
    public record Query : IRequest<AcademicSessionResult>
    {
        public int Id { get; set; }
        public string Term { get; set; } = string.Empty;
    }

    public class Handler : IRequestHandler<Query, AcademicSessionResult>
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

        public async Task<AcademicSessionResult> Handle(
          Query query,
          CancellationToken cancellationToken)
        {
            var academicSession = await _repository.GetAsync(query.Id, cancellationToken);
            var mapped = _mapper.Map<AcademicSession, AcademicSessionResult>(academicSession);

            return mapped;
        }
    }
}

using Api.Domain.Students;
using AutoMapper;
using MediatR;

namespace Api.Features.Students;

public class GetStudents
{
    public record Query : IRequest<List<StudentResult>>
    {
        public List<int> Ids { get; set; }
        public int? InstitutionId { get; set; }
        public List<int> ActiveSessionIds { get; set; }
        public List<int> ActiveClassIds { get; set; }
        public List<int> StatusIds { get; set; }
        public List<int?> StatusReasonIds { get; set; }
        public bool? IsActive { get; set; } = true;
        public string? FirstName { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<StudentResult>>
    {
        private readonly IMapper _mapper;
        private readonly IStudentsRepository _repository;

        public Handler(
          IStudentsRepository repository,
          IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<StudentResult>> Handle(
          Query query,
          CancellationToken cancellationToken)
        {
            List<StudentResult> mappedList = new();
            IEnumerable<Student> list;

            var studentQuery = new StudentQuery
            {
                IsActive = query.IsActive,
                InstitutionId = query.InstitutionId,
                ActiveSessionIds = query.ActiveSessionIds,
                ActiveClassIds = query.ActiveClassIds,
                StatusIds = query.StatusIds,
                StatusReasonIds = query.StatusReasonIds,
                FirstName = query.FirstName
            };
           
            list = await _repository.GetListByQueryAsync(studentQuery, cancellationToken);

            foreach (var student in list)
            {
                var mapped = _mapper.Map<Student, StudentResult>(student);
                mappedList.Add(mapped);
            }

            return mappedList;
        }
    }
}

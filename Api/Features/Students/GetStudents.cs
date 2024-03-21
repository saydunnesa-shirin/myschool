using Api.Domain.Students;
using AutoMapper;
using MediatR;

namespace Api.Features.Students;

public class GetStudents
{
    public record Query : IRequest<List<StudentResult>>
    {
        public bool? IsActive { get; set; }
        public int? InstitutionId { get; set; }
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
                InstitutionId = query.InstitutionId
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

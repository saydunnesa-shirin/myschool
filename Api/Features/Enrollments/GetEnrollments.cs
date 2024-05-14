using Api.Domain.Enrollments;
using AutoMapper;
using MediatR;

namespace Api.Features.Enrollments;

public class GetEnrollments
{
    public record Query : IRequest<List<EnrollmentResult>>
    {
        public bool? IsActive { get; set; }
        public List<int> Ids { get; set; }
        public int InstitutionId { get; set; }
        public List<int> AcademicSessionIds { get; set; }
        public List<int> AcademicClassIds { get; set; }
        public List<int> StudentIds { get; set; }
        public string? StudentName { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<EnrollmentResult>>
    {
        private readonly IMapper _mapper;
        private readonly IEnrollmentsRepository _repository;

        public Handler(
          IEnrollmentsRepository repository,
          IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<EnrollmentResult>> Handle(
          Query query,
          CancellationToken cancellationToken)
        {
            List<EnrollmentResult> mappedList = new();
            IEnumerable<Enrollment> list;

            var enrollmentQuery = new EnrollmentQuery
            {
                IsActive = query.IsActive,
                InstitutionIds = [query.InstitutionId],
                AcademicClassIds = query.AcademicClassIds,
                AcademicSessionIds = query.AcademicSessionIds,
                StudentIds = query.StudentIds,
                StudentName = query.StudentName
            };

            list = await _repository.GetListByQueryAsync(enrollmentQuery, cancellationToken);

            foreach (var enrollment in list)
            {
                var mapped = _mapper.Map<Enrollment, EnrollmentResult>(enrollment);
                mappedList.Add(mapped);
            }

            return mappedList;
        }
    }
}

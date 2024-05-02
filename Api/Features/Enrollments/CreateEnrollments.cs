using Api.Domain.Enrollments;
using AutoMapper;
using MediatR;

namespace Api.Features.Enrollments;

public class CreateEnrollments
{
    public record Command : IRequest<List<EnrollmentResult>>
    {
        public DateTime EnrollmentDate { get; set; }
        public int InstitutionId { get; set; }
        public int AcademicSessionId { get; set; }
        public int AcademicClassId { get; set; }
        public List<int> StudentIds { get; set; }
    }

    public class Handler : IRequestHandler<Command, List<EnrollmentResult>>
    {
        private readonly IMapper _mapper;
        private readonly IEnrollmentsRepository _repository;

        public Handler(
          IEnrollmentsRepository repository,
          IMapper mapper)
        {
            ArgumentNullException.ThrowIfNull(repository);
            ArgumentNullException.ThrowIfNull(mapper);

            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<EnrollmentResult>> Handle(
          Command command,
          CancellationToken cancellationToken)
        {
            List<EnrollmentResult> mappedList = new();

            var enrollmentList = new List<Enrollment>();

            foreach (var id in command.StudentIds)
            {
                var enrollment = new Enrollment
                {
                    EnrollmentDate = command.EnrollmentDate,
                    InstitutionId = command.InstitutionId,
                    AcademicSessionId = command.AcademicSessionId,
                    AcademicClassId = command.AcademicClassId,
                    StudentId = id
                };

                enrollmentList.Add(enrollment);
            }
            
            var saved = await _repository.CreateAsync(enrollmentList, cancellationToken);

            var enrollmentQuery = new EnrollmentQuery
            {
                Ids = saved.Select(e => e.Id).ToList()
            };

            var list = await _repository.GetListByQueryAsync(enrollmentQuery, cancellationToken);

            foreach (var enrollment in list)
            {
                var mapped = _mapper.Map<Enrollment, EnrollmentResult>(enrollment);
                mappedList.Add(mapped);
            }

            return mappedList;
        }
    }
}

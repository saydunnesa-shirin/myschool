using Api.Domain.Enrollments;
using AutoMapper;
using MediatR;

namespace Api.Features.Enrollments;

public class CreateEnrollment
{
    public record Command : IRequest<EnrollmentResult>
    {
        public DateTime EnrollmentDate { get; set; }
        public int InstitutionId { get; set; }
        public int AcademicSessionId { get; set; }
        public int AcademicClassId { get; set; }
        public int StudentId { get; set; }
    }

    public class Handler : IRequestHandler<Command, EnrollmentResult>
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

        public async Task<EnrollmentResult> Handle(
          Command command,
          CancellationToken cancellationToken)
        {
            var studentToCreate = new Enrollment
            {
                EnrollmentDate = command.EnrollmentDate,
                InstitutionId = command.InstitutionId,
                AcademicSessionId = command.AcademicSessionId,
                AcademicClassId = command.AcademicClassId,
                StudentId = command.StudentId
            };
            
            var seved = await _repository.CreateAsync(studentToCreate, cancellationToken);
            var student = await _repository.GetByIdyAsync(seved.Id, cancellationToken);

            var mappedEnrollment = _mapper.Map<Enrollment, EnrollmentResult>(student);

            return mappedEnrollment;
        }
    }
}

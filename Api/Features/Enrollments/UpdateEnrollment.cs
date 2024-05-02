using Api.Infrastructure.Exceptions;
using AutoMapper;
using MediatR;
using Enrollment = Api.Domain.Enrollments.Enrollment;

namespace Api.Features.Enrollments;

public class UpdateEnrollment
{
    public record Command : IRequest<EnrollmentResult>
    {
        public int Id { get; set; }
        public string EnrollmentId { get; set; }
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
            var studentToUpdate = await _repository.GetByIdyAsync(command.Id, cancellationToken) ?? throw new NotFoundException("Not found");

            studentToUpdate.EnrollmentDate = command.EnrollmentDate;
            studentToUpdate.InstitutionId = command.InstitutionId;
            studentToUpdate.AcademicSessionId = command.AcademicSessionId;
            studentToUpdate.AcademicClassId = command.AcademicClassId;
            studentToUpdate.StudentId = command.StudentId;

            studentToUpdate.UpdatedBy = 0;
            studentToUpdate.UpdatedDate = DateTime.UtcNow;

            var seved = await _repository.UpdateAsync(studentToUpdate, cancellationToken);
            var student = await _repository.GetByIdyAsync(seved.Id, cancellationToken);

            var mappedEnrollment = _mapper.Map<Enrollment, EnrollmentResult>(student);

            return mappedEnrollment;
        }
    }
}

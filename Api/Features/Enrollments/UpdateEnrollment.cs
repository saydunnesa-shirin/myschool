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
        public DateTime EnrollmentDate { get; set; }
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
            var toUpdate = await _repository.GetByIdyAsync(command.Id, cancellationToken) ?? throw new NotFoundException("Not found");

            toUpdate.EnrollmentDate = command.EnrollmentDate;
            toUpdate.AcademicSessionId = command.AcademicSessionId;
            toUpdate.AcademicClassId = command.AcademicClassId;

            toUpdate.UpdatedBy = 0;
            toUpdate.UpdatedDate = DateTime.UtcNow;

            var seved = await _repository.UpdateAsync(toUpdate, cancellationToken);
            var updated = await _repository.GetByIdyAsync(seved.Id, cancellationToken);

            var mappedEnrollment = _mapper.Map<Enrollment, EnrollmentResult>(updated);

            return mappedEnrollment;
        }
    }
}

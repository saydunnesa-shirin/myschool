using Api.Infrastructure.Exceptions;
using AutoMapper;
using MediatR;
using Enrollment = Api.Domain.Enrollments.Enrollment;

namespace Api.Features.Enrollments;

public class UpdateEnrollmentWithStudentStatus
{
    public record Command : IRequest<EnrollmentResult>
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        public int StatusReasonId { get; set; }
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
            toUpdate.UpdatedBy = 0;
            toUpdate.UpdatedDate = DateTime.UtcNow;

            var seved = await _repository.UpdateWithStudentStatusAsync(toUpdate, cancellationToken, command.StatusId, command.StatusReasonId);
            var updated = await _repository.GetByIdyAsync(seved.Id, cancellationToken);

            var mappedEnrollment = _mapper.Map<Enrollment, EnrollmentResult>(updated);

            return mappedEnrollment;
        }
    }
}

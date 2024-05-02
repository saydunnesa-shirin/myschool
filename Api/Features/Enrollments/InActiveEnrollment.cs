using Api.Infrastructure.Exceptions;
using AutoMapper;
using MediatR;
using Enrollment = Api.Domain.Enrollments.Enrollment;

namespace Api.Features.Enrollments;

public class InActiveEnrollment
{
    public record Command : IRequest<EnrollmentResult>
    {
        public int Id { get; set; }
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
            var studentToInActive = await _repository.GetByIdyAsync(command.Id, cancellationToken) ?? throw new NotFoundException("Not found");
            studentToInActive.IsActive = false;
            studentToInActive.UpdatedBy = 0;
            studentToInActive.UpdatedDate = DateTime.UtcNow;

            var seved = await _repository.UpdateAsync(studentToInActive, cancellationToken);
            var student = await _repository.GetByIdyAsync(seved.Id, cancellationToken);

            var mappedEnrollment = _mapper.Map<Enrollment, EnrollmentResult>(student);

            return mappedEnrollment;
        }
    }
}

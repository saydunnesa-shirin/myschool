using Api.Domain.Enrollments;
using AutoMapper;
using MediatR;

namespace Api.Features.Enrollments;

public class GetEnrollment
{
    public record Query : IRequest<EnrollmentResult>
    {
        public int Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, EnrollmentResult>
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

        public async Task<EnrollmentResult> Handle(
          Query query,
          CancellationToken cancellationToken)
        {
            var student = await _repository.GetByIdyAsync(query.Id, cancellationToken);
            var mappedEnrollment = _mapper.Map<Enrollment, EnrollmentResult>(student);

            return mappedEnrollment;
        }
    }
}

using AutoMapper;
using MediatR;

namespace Api.Features.Enrollments;

public class DeleteEnrollment
{
    public record Command : IRequest<int?>
    {
        public int Id { get; set; }
    }
    public class Handler : IRequestHandler<Command, int?>
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

        public async Task<int?> Handle(
          Command command,
          CancellationToken cancellationToken)
        {
            var deleted = await _repository.DeleteAsync(command.Id, cancellationToken);
            return deleted;
        }
    }
}

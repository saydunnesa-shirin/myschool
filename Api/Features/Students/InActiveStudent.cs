using Api.Infrastructure.Exceptions;
using AutoMapper;
using MediatR;
using Student = Api.Domain.Students.Student;

namespace Api.Features.Students;

public class InActiveStudent
{
    public record Command : IRequest<StudentResult>
    {
        public int Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, StudentResult>
    {
        private readonly IMapper _mapper;
        private readonly IStudentsRepository _repository;

        public Handler(
          IStudentsRepository repository,
          IMapper mapper)
        {
            ArgumentNullException.ThrowIfNull(repository);
            ArgumentNullException.ThrowIfNull(mapper);

            _repository = repository;
            _mapper = mapper;
        }

        public async Task<StudentResult> Handle(
          Command command,
          CancellationToken cancellationToken)
        {
            var studentToInActive = await _repository.GetByIdyAsync(command.Id, cancellationToken) ?? throw new NotFoundException("Not found");
            studentToInActive.IsActive = false;
            studentToInActive.UpdatedBy = 0;
            studentToInActive.UpdatedDate = DateTime.UtcNow;

            var seved = await _repository.UpdateAsync(studentToInActive, cancellationToken);
            var student = await _repository.GetByIdyAsync(seved.Id, cancellationToken);

            var mappedStudent = _mapper.Map<Student, StudentResult>(student);

            return mappedStudent;
        }
    }
}

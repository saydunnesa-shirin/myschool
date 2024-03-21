using Api.Domain.Students;
using AutoMapper;
using MediatR;

namespace Api.Features.Students;

public class GetStudent
{
    public record Query : IRequest<StudentResult>
    {
        public int Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, StudentResult>
    {
        private readonly IMapper _mapper;
        private readonly IStudentsRepository _repository;

        public Handler(
          IStudentsRepository repository,
          IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<StudentResult> Handle(
          Query query,
          CancellationToken cancellationToken)
        {
            var student = await _repository.GetByIdyAsync(query.Id, cancellationToken);
            var mappedStudent = _mapper.Map<Student, StudentResult>(student);

            return mappedStudent;
        }
    }
}

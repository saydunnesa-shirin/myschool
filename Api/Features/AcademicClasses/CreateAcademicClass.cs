using Api.Domain.AcademicClasses;
using AutoMapper;
using MediatR;

namespace Api.Features.AcademicClasses;

public class CreateAcademicClass
{
    public record Command : IRequest<Result>
    {
        public int Id { get; set; }
        public int InstitutionId { get; set; }
        public int AcademicSessionId { get; set; }
        public int? TeacherId { get; set; }
        public string Name { get; set; }
    }

    public record Result : BaseResult
    {
    }

    public class Handler : IRequestHandler<Command, Result>
    {
        private readonly IMapper _mapper;
        private readonly IAcademicClassesRepository _repository;

        public Handler(
          IAcademicClassesRepository repository,
          IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(
          Command command,
          CancellationToken cancellationToken)
        {
            var @new = new AcademicClass
            {
                Id = command.Id,
                Name = command.Name,
                InstitutionId = command.InstitutionId,
                AcademicSessionId = command.AcademicSessionId,
                TeacherId = command.TeacherId
            };
            var saved = await _repository.CreateAsync(@new, cancellationToken);

            var academicClass = await _repository.GetAsync(saved.Id, cancellationToken);

            var mappedAcademicClass = _mapper.Map<AcademicClassViewModel, Result>(academicClass);

            return mappedAcademicClass;
        }
    }
}

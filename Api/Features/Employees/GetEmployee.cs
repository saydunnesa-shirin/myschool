using Api.Domain.Employees;
using AutoMapper;
using MediatR;

namespace Api.Features.Employees;

public class GetEmployee
{
    public record Query : IRequest<Result>
    {
        public int Id { get; set; }
    }

    public record Result : BaseResult
    {
    }

    public class Handler : IRequestHandler<Query, Result>
    {
        private readonly IMapper _mapper;
        private readonly IEmployeesRepository _repository;

        public Handler(
          IEmployeesRepository repository,
          IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(
          Query query,
          CancellationToken cancellationToken)
        {
            var employee = await _repository.GetByIdyAsync(query.Id, cancellationToken);
            var mappedEmployee = _mapper.Map<EmployeeViewModel, Result>(employee);

            return mappedEmployee;
        }
    }
}

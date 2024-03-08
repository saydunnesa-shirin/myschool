using Api.Domain.Employees;
using AutoMapper;
using MediatR;

namespace Api.Features.Employees;

public class GetEmployee
{
    public record Query : IRequest<EmployeeResult>
    {
        public int Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, EmployeeResult>
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

        public async Task<EmployeeResult> Handle(
          Query query,
          CancellationToken cancellationToken)
        {
            var employee = await _repository.GetByIdyAsync(query.Id, cancellationToken);
            var mappedEmployee = _mapper.Map<Employee, EmployeeResult>(employee);

            return mappedEmployee;
        }
    }
}

using Api.Domain.Employees;
using AutoMapper;
using MediatR;

namespace Api.Features.Employees;

public class GetEmployees
{
    public record Query : IRequest<List<Result>>
    {

    }

    public record Result : BaseResult
    {
    }

    public class Handler : IRequestHandler<Query, List<Result>>
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

        public async Task<List<Result>> Handle(
          Query query,
          CancellationToken cancellationToken)
        {
            List<Result> mappedEmployees = new List<Result>();
            var employees =
              await _repository.GetListByQueryAsync(cancellationToken);

            foreach (var employee in employees)
            {
                var mappedEmployee = _mapper.Map<Employee, Result>(employee);
                mappedEmployees.Add(mappedEmployee);
            }

            return mappedEmployees;
        }
    }
}

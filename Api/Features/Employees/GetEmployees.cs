using Api.Domain.Employees;
using AutoMapper;
using MediatR;

namespace Api.Features.Employees;

public class GetEmployees
{
    public record Query : IRequest<List<EmployeeResult>>
    {
        public int? DesignationId { get; set; }
        public int? InstitutionId { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<EmployeeResult>>
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

        public async Task<List<EmployeeResult>> Handle(
          Query query,
          CancellationToken cancellationToken)
        {
            List<EmployeeResult> mappedList = new();
            IEnumerable<Employee> list;

            var employeeQuery = new EmployeeQuery
            {
                DesignationId = query.DesignationId,
                InstitutionId = query.InstitutionId
            };
           
            list = await _repository.GetListByQueryAsync(employeeQuery, cancellationToken);

            foreach (var employee in list)
            {
                var mapped = _mapper.Map<Employee, EmployeeResult>(employee);
                mappedList.Add(mapped);
            }

            return mappedList;
        }
    }
}

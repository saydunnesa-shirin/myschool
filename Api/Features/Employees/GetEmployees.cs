using Api.Domain.Employees;
using AutoMapper;
using MediatR;

namespace Api.Features.Employees;

public class GetEmployees
{
    public record Query : IRequest<List<Result>>
    {
        public int? DesignationId { get; set; }
        public int? InstitutionId { get; set; }
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
            List<Result> mappedList = new();
            IEnumerable<EmployeeViewModel> list;

            var employeeQuery = new EmployeeQuery
            {
                DesignationId = query.DesignationId,
                InstitutionId = query.InstitutionId
            };
           
            list = await _repository.GetListByQueryAsync(employeeQuery, cancellationToken);

            foreach (var employee in list)
            {
                var mapped = _mapper.Map<EmployeeViewModel, Result>(employee);
                mappedList.Add(mapped);
            }

            return mappedList;
        }
    }
}

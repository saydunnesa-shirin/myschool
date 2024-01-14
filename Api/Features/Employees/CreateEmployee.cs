using Api.Domain.Employees;
using AutoMapper;
using MediatR;

namespace Api.Features.Employees;

public class CreateEmployee
{
  public record Command : IRequest<Result>
  {
    public string FirstName { get; set; }
    public string LastName {  get; set; }
  }

  public record Result
  {
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
  }

  public class Handler : IRequestHandler<Command, Result>
  {
    private readonly IMapper _mapper;
    private readonly IEmployeesRepository _repository;

    public Handler(
      IEmployeesRepository repository,
      IMapper mapper)
    {
      ArgumentNullException.ThrowIfNull(repository);
      ArgumentNullException.ThrowIfNull(mapper);

      _repository = repository;
      _mapper = mapper;
    }

    public async Task<Result> Handle(
      Command command,
      CancellationToken cancellationToken)
    {
      var employeeToCreate = new Employee
      {
        FirstName = command.FirstName,
        LastName = command.LastName
      };
      await _repository.CreateEmployeeAsync(employeeToCreate, cancellationToken);
      var mappedEmployee = _mapper.Map<Employee, Result>(employeeToCreate);

      return mappedEmployee;
    }
  }
}

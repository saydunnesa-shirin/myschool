using Api.Common;
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
      ArgumentNullException.ThrowIfNull(repository);
      ArgumentNullException.ThrowIfNull(mapper);

      _repository = repository;
      _mapper = mapper;
    }

    public async Task<Result> Handle(
      Query query,
      CancellationToken cancellationToken)
    {
      var employee =
        await _repository.GetEmployeeAsync(query.Id, cancellationToken);
      var mappedEmployee = _mapper.Map<Employee, Result>(employee);

      return mappedEmployee;
    }
  }
}

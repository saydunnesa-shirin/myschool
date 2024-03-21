using Api.Domain.Employees;
using Api.Infrastructure.Exceptions;
using AutoMapper;
using MediatR;

namespace Api.Features.Employees;

public class InActiveEmployee
{
    public record Command : IRequest<EmployeeResult>
    {
        public int Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, EmployeeResult>
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

        public async Task<EmployeeResult> Handle(
          Command command,
          CancellationToken cancellationToken)
        {
            var employeeToUpdate = await _repository.GetByIdyAsync(command.Id, cancellationToken) ?? throw new NotFoundException("Not found");
            employeeToUpdate.IsActive = false;
            employeeToUpdate.UpdatedBy = 0;
            employeeToUpdate.UpdatedDate = DateTime.UtcNow;

            var seved = await _repository.UpdateAsync(employeeToUpdate, cancellationToken);
            var employee = await _repository.GetByIdyAsync(seved.Id, cancellationToken);

            var mappedEmployee = _mapper.Map<Employee, EmployeeResult>(employee);

            return mappedEmployee;
        }
    }
}

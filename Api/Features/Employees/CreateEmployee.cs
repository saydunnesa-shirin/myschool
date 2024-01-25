using Api.Domain.Employees;
using AutoMapper;
using MediatR;

namespace Api.Features.Employees;

public class CreateEmployee
{
    public record Command : IRequest<Result>
    {
        public int InstitutionId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public int GenderId { get; set; }
        public DateTime JoinDate { get; set; }
        public string Mobile { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int EmployeeTypeId { get; set; }
        public int DesignationId { get; set; }
        public int BloodGroupId { get; set; }
        public string EmployeeId { get; set; } = string.Empty;
    }

    public record Result : BaseResult
    {
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
                InstitutionId = command.InstitutionId,
                FirstName = command.FirstName,
                LastName = command.LastName,
                Title = command.Title,
                DateOfBirth = command.DateOfBirth,
                GenderId = command.GenderId,
                JoinDate = command.JoinDate,
                Mobile = command.Mobile,
                Email = command.Email,
                EmployeeTypeId = command.EmployeeTypeId,
                DesignationId = command.DesignationId,
                BloodGroupId = command.BloodGroupId,
                EmployeeId = command.EmployeeId
            };
            await _repository.CreateAsync(employeeToCreate, cancellationToken);
            var mappedEmployee = _mapper.Map<Employee, Result>(employeeToCreate);

            return mappedEmployee;
        }
    }
}

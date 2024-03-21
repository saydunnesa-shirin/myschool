using Api.Domain.Employees;
using Api.Infrastructure.Exceptions;
using AutoMapper;
using MediatR;

namespace Api.Features.Employees;

public class UpdateEmployee
{
    public record Command : IRequest<EmployeeResult>
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public int EmployeeTypeId { get; set; }
        public DateTime JoinDate { get; set; }
        public int DesignationId { get; set; }
        public int InstitutionId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public int? GenderId { get; set; }
        public int? BloodGroupId { get; set; }

        // Additional
        public int? CountryId { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }

        // Other information
        public string? MotherName { get; set; }
        public string? FatherName { get; set; }
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

            employeeToUpdate.EmployeeId = command.EmployeeId;
            employeeToUpdate.JoinDate = command.JoinDate;
            employeeToUpdate.EmployeeTypeId = command.EmployeeTypeId;
            employeeToUpdate.DesignationId = command.DesignationId;
            employeeToUpdate.InstitutionId = command.InstitutionId;

            employeeToUpdate.FirstName = command.FirstName;
            employeeToUpdate.LastName = command.LastName;
            employeeToUpdate.Mobile = command.Mobile;
            employeeToUpdate.Email = command.Email;
            employeeToUpdate.DateOfBirth = command.DateOfBirth;
            employeeToUpdate.GenderId = command.GenderId;
            employeeToUpdate.BloodGroupId = command.BloodGroupId;
            employeeToUpdate.CountryId = command.CountryId;
            employeeToUpdate.Street = command.Street;
            employeeToUpdate.City = command.City;
            employeeToUpdate.State = command.State;
            employeeToUpdate.PostalCode = command.PostalCode;
                
            employeeToUpdate.FatherName = command.FatherName;
            employeeToUpdate.MotherName = command.MotherName;
            employeeToUpdate.UpdatedBy = 0;
            employeeToUpdate.UpdatedDate = DateTime.UtcNow;

            var seved = await _repository.UpdateAsync(employeeToUpdate, cancellationToken);
            var employee = await _repository.GetByIdyAsync(seved.Id, cancellationToken);

            var mappedEmployee = _mapper.Map<Employee, EmployeeResult>(employee);

            return mappedEmployee;
        }
    }
}

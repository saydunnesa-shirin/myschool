using Api.Domain.Employees;
using AutoMapper;
using MediatR;

namespace Api.Features.Employees;

public class CreateEmployee
{
    public record Command : IRequest<EmployeeResult>
    {
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
            var employeeToCreate = new Employee
            {
                EmployeeId = command.EmployeeId,
                JoinDate = command.JoinDate,
                EmployeeTypeId = command.EmployeeTypeId,
                DesignationId = command.DesignationId,
                InstitutionId = command.InstitutionId,

                FirstName = command.FirstName,
                LastName = command.LastName,
                Mobile = command.Mobile,
                Email = command.Email,
                DateOfBirth = command.DateOfBirth,
                GenderId = command.GenderId,
                BloodGroupId = command.BloodGroupId,
                CountryId = command.CountryId,
                Street = command.Street,
                City = command.City,
                State = command.State,
                PostalCode = command.PostalCode,
                
                FatherName = command.FatherName,
                MotherName = command.MotherName
            };
            
            var seved = await _repository.CreateAsync(employeeToCreate, cancellationToken);
            var employee = await _repository.GetByIdyAsync(seved.Id, cancellationToken);

            var mappedEmployee = _mapper.Map<Employee, EmployeeResult>(employee);

            return mappedEmployee;
        }
    }
}

using Api.Domain.Students;
using AutoMapper;
using MediatR;

namespace Api.Features.Students;

public class CreateStudent
{
    public record Command : IRequest<StudentResult>
    {
        public string StudentId { get; set; } 
        public DateTime AdmissionDate { get; set; }
        public int InstitutionId { get; set; }
        public int ActiveSessionId { get; set; }
        public int ActiveClassId { get; set; }
        public int StatusId { get; set; }
        public int? StatusReasonId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? GenderId { get; set; }
        public int? BloodGroupId { get; set; }

        // Additional
        public int? CountryId { get; set; }
        public string? Street { get; set; } 
        public string City { get; set; } 
        public string State { get; set; } 
        public string PostalCode { get; set; } 

        // Other information
        public string MotherName { get; set; } 
        public string FatherName { get; set; } 
    }

    public class Handler : IRequestHandler<Command, StudentResult>
    {
        private readonly IMapper _mapper;
        private readonly IStudentsRepository _repository;

        public Handler(
          IStudentsRepository repository,
          IMapper mapper)
        {
            ArgumentNullException.ThrowIfNull(repository);
            ArgumentNullException.ThrowIfNull(mapper);

            _repository = repository;
            _mapper = mapper;
        }

        public async Task<StudentResult> Handle(
          Command command,
          CancellationToken cancellationToken)
        {
            var studentToCreate = new Student
            {
                StudentId = command.StudentId,
                AdmissionDate = command.AdmissionDate,
                InstitutionId = command.InstitutionId,
                ActiveSessionId = command.ActiveSessionId,
                ActiveClassId = command.ActiveClassId,
                StatusId = command.StatusId,
                StatusReasonId = command.StatusReasonId,

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
            
            var seved = await _repository.CreateAsync(studentToCreate, cancellationToken);
            var student = await _repository.GetByIdyAsync(seved.Id, cancellationToken);

            var mappedStudent = _mapper.Map<Student, StudentResult>(student);

            return mappedStudent;
        }
    }
}

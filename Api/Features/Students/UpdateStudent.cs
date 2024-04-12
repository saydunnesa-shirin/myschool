using Api.Infrastructure.Exceptions;
using AutoMapper;
using MediatR;
using Student = Api.Domain.Students.Student;

namespace Api.Features.Students;

public class UpdateStudent
{
    public record Command : IRequest<StudentResult>
    {
        public int Id { get; set; }
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
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }

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
            var studentToUpdate = await _repository.GetByIdyAsync(command.Id, cancellationToken) ?? throw new NotFoundException("Not found");

            studentToUpdate.StudentId = command.StudentId;
            studentToUpdate.AdmissionDate = command.AdmissionDate;
            studentToUpdate.InstitutionId = command.InstitutionId;
            studentToUpdate.ActiveSessionId = command.ActiveSessionId;
            studentToUpdate.ActiveClassId = command.ActiveClassId;
            studentToUpdate.StatusId = command.StatusId;
            studentToUpdate.StatusReasonId = command.StatusReasonId;

            studentToUpdate.FirstName = command.FirstName;
            studentToUpdate.LastName = command.LastName;
            studentToUpdate.Mobile = command.Mobile;
            studentToUpdate.Email = command.Email;
            studentToUpdate.DateOfBirth = command.DateOfBirth;
            studentToUpdate.GenderId = command.GenderId;
            studentToUpdate.BloodGroupId = command.BloodGroupId;
            studentToUpdate.CountryId = command.CountryId;
            studentToUpdate.Street = command.Street;
            studentToUpdate.City = command.City;
            studentToUpdate.State = command.State;
            studentToUpdate.PostalCode = command.PostalCode;

            studentToUpdate.FatherName = command.FatherName;
            studentToUpdate.MotherName = command.MotherName;
            studentToUpdate.UpdatedBy = 0;
            studentToUpdate.UpdatedDate = DateTime.UtcNow;

            var seved = await _repository.UpdateAsync(studentToUpdate, cancellationToken);
            var student = await _repository.GetByIdyAsync(seved.Id, cancellationToken);

            var mappedStudent = _mapper.Map<Student, StudentResult>(student);

            return mappedStudent;
        }
    }
}

using Api.Data.Entities;
using Api.Domain.AcademicClasses;
using Api.Domain.Countries;
using Api.Domain.Institutions;

namespace Api.Domain.Employees;

public class Employee : BaseEntity
{
    public string EmployeeId { get; set; } = string.Empty;
    public int InstitutionId { get; set; }
    public DateTime JoinDate { get; set; }
    public int EmployeeTypeId { get; set; }
    public int DesignationId { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Mobile { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public DateTime? DateOfBirth { get; set; }
    public int? GenderId { get; set; }
    public int? BloodGroupId { get; set; }

    // Additional
    public int? CountryId {  get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }

    // Other information
    public string? MotherName { get; set; }
    public string? FatherName { get; set; }

    public virtual Country Country { get; set; }

    public virtual Institution Institution { get; set; }

    public virtual ICollection<AcademicClass> AcademicClasses { get; set; }
}

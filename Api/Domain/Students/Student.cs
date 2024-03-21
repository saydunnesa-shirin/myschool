using Api.Data.Entities;
using Api.Domain.Countries;
using Api.Domain.Institutions;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Students;

public class Student : BaseEntity
{
    public string? StudentId { get; set; } = string.Empty;
    public int InstitutionId { get; set; }
    public DateTime AdmissionDate { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Mobile { get; set; }
    public string? Email { get; set; }

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
    public string MotherName { get; set; }
    public string FatherName { get; set; }

    public virtual Country Country { get; set; }

    public virtual Institution Institution { get; set; }

    //public virtual ICollection<AcademicClass> AcademicClasses { get; set; }
}

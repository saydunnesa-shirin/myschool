using Api.Data.Entities;
using Api.Domain.AcademicClasses;
using Api.Domain.AcademicSessions;
using Api.Domain.Countries;
using Api.Domain.Institutions;

namespace Api.Domain.Students;

public class Student : BaseEntity
{
    public string? StudentId { get; set; } = string.Empty;
    public int InstitutionId { get; set; }
    public DateTime AdmissionDate { get; set; }

    // Academic Information
    public int ActiveSessionId { get; set; }
    public int ActiveClassId { get; set; }
    public int StatusId { get; set; }
    public int? StatusReasonId { get; set; }

    // Personal Information
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

    public string MotherName { get; set; }
    public string FatherName { get; set; }

    public virtual Country Country { get; set; }
    public virtual Institution Institution { get; set; }
    public virtual AcademicSession AcademicSession { get; set; }
    public virtual AcademicClass AcademicClass { get; set; }
}

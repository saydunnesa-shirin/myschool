namespace Api.Features.Students;

public record StudentResult
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
    public bool IsActive { get; set; }
    public string? CountryName { get; set; }
    public string InstitutionName { get; set; }

    public string ActiveSessionName { get; set; }
    public string ActiveClassName { get; set; }
    public string Status { get; set; }
    public string? StatusReason { get; set; }

    public DateTime CreatedDate { get; set; }
    public int CreatedBy { get; set; }
    public DateTime UpdatedDate { get; set; }
    public int UpdatedBy { get; set; }

}

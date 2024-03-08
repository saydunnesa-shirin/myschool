namespace Api.Features.Employees;

public record EmployeeResult
{
    public int Id { get; set; }
    public string EmployeeId { get; set; } = string.Empty;
    public int EmployeeTypeId { get; set; }
    public DateTime JoinDate { get; set; }
    public int DesignationId { get; set; }
    public int InstitutionId { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string Mobile { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public int GenderId { get; set; }
    public int BloodGroupId { get; set; }

    // Additional
    public int CountryId { get; set; }
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;

    // Other information
    public string MotherName { get; set; } = string.Empty;
    public string FatherName { get; set; } = string.Empty;
    public string CountryName { get; set; }
    public string InstitutionName { get; set; }
}

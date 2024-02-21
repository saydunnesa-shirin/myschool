using Api.Data.Entities;

namespace Api.Domain.Employees;

public class EmployeeViewModel : BaseEntity
{
    public string EmployeeId { get; set; }
    public int InstitutionId { get; set; }
    public DateTime JoinDate { get; set; }
    public int EmployeeTypeId { get; set; }
    public int DesignationId { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Mobile { get; set; }
    public string Email { get; set; }

    public DateTime? DateOfBirth { get; set; }
    public int? GenderId { get; set; }
    public int? BloodGroupId { get; set; }

    // Additional
    public int? CountryId {  get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }

    // Other information
    public string MotherName { get; set; }
    public string FatherName { get; set; }

    public string CountryName { get; set; }
}

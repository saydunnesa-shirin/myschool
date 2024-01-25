using Api.Common;
using Api.Data.Entities;

namespace Api.Domain.Employees;

public class Employee : BaseEntity
{
    public int Id { get; set; }
    public int InstitutionId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public int GenderId { get; set; }
    public DateTime JoinDate { get; set; }
    public string Mobile { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int EmployeeTypeId { get; set; }
    public int DesignationId { get; set; }
    public int BloodGroupId { get; set; }
    public string EmployeeId { get; set; } = string.Empty;

    // Additional
    public string Address { get; set; } = string.Empty;
    // other information
    public string MotherName { get; set; } = string.Empty;
    public string FatherName { get; set; } = string.Empty;
}

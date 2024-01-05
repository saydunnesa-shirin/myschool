using Api.Common;

namespace Api.Domain.Employees;

public class Employee
{
  public int Id { get; set; }
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public string Title { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string Mobile { get; set; } = string.Empty;
  public DateTime DateOfBirth { get; set; }
  public DateTime JoinDate { get; set; }

  public string Address { get; set; } = string.Empty;
  //public 

  // other information
  public EmployeeType EmployeeType { get; set; }
  public string MotherName { get; set; } = string.Empty;
  public string FatherName { get; set; } = string.Empty;
  public Gender Gender { get; set; }
  public string BloodGroup { get; set; } = string.Empty;
}

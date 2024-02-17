using Api.Data.Entities;
using Api.Domain.Employees;
using Api.Domain.Institutions;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.Employees;

public interface IEmployeesRepository
{
    Task<Employee> CreateAsync(Employee newEmployee, CancellationToken cancellationToken);
    Task<int?> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<EmployeeViewModel>> GetListByQueryAsync(CancellationToken cancellationToken);
    Task<EmployeeViewModel> GetByIdyAsync(int id, CancellationToken cancellationToken);
    Task<Employee> GetAsync(int Id, CancellationToken cancellationToken);
    Task<Employee> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<Employee> UpdateAsync(Employee employee, CancellationToken cancellationToken);
}

public class EmployeesRepository : IEmployeesRepository
{
    private readonly ILogger<EmployeesRepository> _logger;
    private readonly MySchoolContext _context;

    public EmployeesRepository(
      ILogger<EmployeesRepository> logger,
      MySchoolContext context)
    {
        ArgumentNullException.ThrowIfNull(logger);

        _logger = logger;
        _context = context;
    }

    public int Id { get; private set; }

    public async Task<Employee> CreateAsync(Employee newEmployee, CancellationToken cancellationToken)
    {
        try
        {
            _ = _context.Employees.Add(newEmployee);
            _ = await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
        return newEmployee;
    }

    public async Task<int?> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        Employee deletableEmployee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (deletableEmployee != null)
        {
            _ = _context.Employees.Remove(deletableEmployee);
            _ = await _context.SaveChangesAsync(cancellationToken);
            return id;
        }
        return null;
    }

    public async Task<IEnumerable<EmployeeViewModel>> GetListByQueryAsync(CancellationToken cancellationToken)
    {
        var result = (
                from i in _context.Employees
                join c in _context.Countries on i.CountryId equals c.Id
                select new EmployeeViewModel
                {
                    Id = i.Id,
                    CountryId = i.CountryId,
                    EmployeeId = i.EmployeeId,
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    BloodGroupId = i.BloodGroupId,
                    City = i.City,
                    CreatedBy = i.CreatedBy,
                    CreatedDate = i.CreatedDate,
                    DateOfBirth = i.DateOfBirth,
                    DesignationId = i.DesignationId,
                    Email = i.Email,
                    EmployeeTypeId = i.EmployeeTypeId,
                    FatherName = i.FatherName,
                    MotherName = i.MotherName,
                    GenderId = i.GenderId,
                    InstitutionId = i.InstitutionId,
                    IsActive = i.IsActive,
                    JoinDate = i.JoinDate,
                    Mobile = i.Mobile,
                    PostalCode = i.PostalCode,
                    State = i.State,
                    Street = i.Street,
                    UpdatedBy = i.UpdatedBy,
                    UpdatedDate = i.UpdatedDate,

                    CountryName = c.Name
                }
                ).ToListAsync(cancellationToken);

        return await result;
    }

    public async Task<Employee> GetAsync(
      int Id,
      CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting employee information");

        Employee employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);

        return employee;
    }

    public Task<Employee> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<EmployeeViewModel> GetByIdyAsync(int id, CancellationToken cancellationToken)
    {
        var result = (
                from i in _context.Employees
                join c in _context.Countries on i.CountryId equals c.Id
                where i.Id == id
                select new EmployeeViewModel
                {
                    Id = i.Id,
                    CountryId = i.CountryId,
                    EmployeeId = i.EmployeeId,
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    BloodGroupId = i.BloodGroupId,
                    City = i.City,
                    CreatedBy = i.CreatedBy,
                    CreatedDate = i.CreatedDate,    
                    DateOfBirth = i.DateOfBirth,
                    DesignationId = i.DesignationId,
                    Email = i.Email,
                    EmployeeTypeId = i.EmployeeTypeId,
                    FatherName = i.FatherName,
                    MotherName = i.MotherName,
                    GenderId = i.GenderId,
                    InstitutionId = i.InstitutionId,
                    IsActive = i.IsActive,
                    JoinDate = i.JoinDate,
                    Mobile = i.Mobile,
                    PostalCode = i.PostalCode,
                    State = i.State,
                    Street = i.Street,
                    UpdatedBy = i.UpdatedBy,
                    UpdatedDate = i.UpdatedDate,
                   
                    CountryName = c.Name
                }
                ).FirstOrDefaultAsync(cancellationToken);

        return await result;
    }

    public async Task<Employee> UpdateAsync(Employee employee, CancellationToken cancellationToken)
    {
        try
        {
            _ = _context.Employees.Update(employee);
            _ = await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
        return employee;
    }
}

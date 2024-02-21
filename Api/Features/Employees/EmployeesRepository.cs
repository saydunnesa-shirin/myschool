using Api.Data.Entities;
using Api.Domain.Employees;
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
        try
        {
            Employee deletableEmployee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (deletableEmployee != null)
            {
                _ = _context.Employees.Remove(deletableEmployee);
                _ = await _context.SaveChangesAsync(cancellationToken);
                return id;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
        
        return null;
    }

    public async Task<IEnumerable<EmployeeViewModel>> GetListByQueryAsync(CancellationToken cancellationToken)
    {
        var result = (
                from e in _context.Employees
                join c in _context.Countries on e.CountryId equals c.Id
                select new EmployeeViewModel
                {
                    Id = e.Id,
                    CountryId = e.CountryId,
                    EmployeeId = e.EmployeeId,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    BloodGroupId = e.BloodGroupId,
                    City = e.City,
                    CreatedBy = e.CreatedBy,
                    CreatedDate = e.CreatedDate,
                    DateOfBirth = e.DateOfBirth,
                    DesignationId = e.DesignationId,
                    Email = e.Email,
                    EmployeeTypeId = e.EmployeeTypeId,
                    FatherName = e.FatherName,
                    MotherName = e.MotherName,
                    GenderId = e.GenderId,
                    InstitutionId = e.InstitutionId,
                    IsActive = e.IsActive,
                    JoinDate = e.JoinDate,
                    Mobile = e.Mobile,
                    PostalCode = e.PostalCode,
                    State = e.State,
                    Street = e.Street,
                    UpdatedBy = e.UpdatedBy,
                    UpdatedDate = e.UpdatedDate,

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
                from e in _context.Employees
                join c in _context.Countries on e.CountryId equals c.Id
                where e.Id == id
                select new EmployeeViewModel
                {
                    Id = e.Id,
                    CountryId = e.CountryId,
                    EmployeeId = e.EmployeeId,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    BloodGroupId = e.BloodGroupId,
                    City = e.City,
                    CreatedBy = e.CreatedBy,
                    CreatedDate = e.CreatedDate,    
                    DateOfBirth = e.DateOfBirth,
                    DesignationId = e.DesignationId,
                    Email = e.Email,
                    EmployeeTypeId = e.EmployeeTypeId,
                    FatherName = e.FatherName,
                    MotherName = e.MotherName,
                    GenderId = e.GenderId,
                    InstitutionId = e.InstitutionId,
                    IsActive = e.IsActive,
                    JoinDate = e.JoinDate,
                    Mobile = e.Mobile,
                    PostalCode = e.PostalCode,
                    State = e.State,
                    Street = e.Street,
                    UpdatedBy = e.UpdatedBy,
                    UpdatedDate = e.UpdatedDate,
                   
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

using Api.Data.Entities;
using Api.Domain.Employees;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.Employees;

public interface IEmployeesRepository
{
    Task<Employee> CreateAsync(Employee newEmployee, CancellationToken cancellationToken);
    Task<Employee> UpdateAsync(Employee employee, CancellationToken cancellationToken);
    Task<int?> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<Employee>> GetListByQueryAsync(EmployeeQuery query, CancellationToken cancellationToken);
    Task<Employee> GetByIdyAsync(int id, CancellationToken cancellationToken);
    Task<Employee> GetByEmailAsync(string email, CancellationToken cancellationToken);
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

    public async Task<Employee> UpdateAsync(Employee employee, CancellationToken cancellationToken)
    {
        try
        {
            _ = await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
        return employee;
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

    public async Task<IEnumerable<Employee>> GetListByQueryAsync(EmployeeQuery query, CancellationToken cancellationToken)
    {
        var result = await _context.Employees
            .Where(x => (query.DesignationId == null || x.DesignationId == query.DesignationId) &&
                        (query.InstitutionId == null || x.InstitutionId == query.InstitutionId)) 
            .Include(x => x.Institution)
            .Include(x => x.Country)
            .ToListAsync(cancellationToken);

        return result;
    }

    public async Task<Employee> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var result = await _context.Employees
            .Where(x => x.Email == email)
            .Include(x => x.Institution)
            .Include(x => x.Country)
            .FirstOrDefaultAsync(cancellationToken);

        return result;
    }

    public async Task<Employee> GetByIdyAsync(int id, CancellationToken cancellationToken)
    {
        var result = await _context.Employees
            .Where(x => x.Id == id)
            .Include(x => x.Institution)
            .Include(x => x.Country)
            .FirstOrDefaultAsync(cancellationToken);

        return result;
    }
}

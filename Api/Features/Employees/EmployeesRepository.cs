using Api.Data.Entities;
using Api.Domain.Employees;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.Employees;

public interface IEmployeesRepository
{
    Task<Employee> CreateAsync(Employee newEmployee, CancellationToken cancellationToken);
    Task<int?> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<Employee>> GetListByQueryAsync(CancellationToken cancellationToken);
    Task<Employee> GetAsync(int Id, CancellationToken cancellationToken);
    Task<Employee> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<Employee> UpdateAsync(int id, Employee employee, CancellationToken cancellationToken);
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
        _ = _context.Employees.Add(newEmployee);
        _ = await _context.SaveChangesAsync(cancellationToken);
        return newEmployee;
    }

    public async Task<int?> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        Employee deletableEmployee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        if (deletableEmployee != null)
        {
            _ = _context.Employees.Remove(deletableEmployee);
            _ = await _context.SaveChangesAsync(cancellationToken);
            return id;
        }
        return null;
    }

    public async Task<IEnumerable<Employee>> GetListByQueryAsync(CancellationToken cancellationToken)
    {
        return await _context.Employees.ToListAsync(cancellationToken);
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

    public Task<Employee> UpdateAsync(int id, Employee employee, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

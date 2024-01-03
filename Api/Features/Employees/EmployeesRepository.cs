using Api.Domain.Employees;

namespace Api.Features.Employees;

public interface IEmployeesRepository
{
    Task<Employee> GetEmployeeyAsync(
            int Id,
            CancellationToken cancellationToken);
}

public class EmployeesRepository : IEmployeesRepository
{
    private readonly ILogger<EmployeesRepository> _logger;

    public EmployeesRepository(ILogger<EmployeesRepository> logger)
    {
        ArgumentNullException.ThrowIfNull(logger);

        _logger = logger;
    }

    public async Task<Employee> GetEmployeeyAsync(
        int Id, 
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting employee information");

        await Task.CompletedTask;

        return new Employee
        {
            Id = 999,
            FirstName = "Ghost",
            LastName = "Employee"
        };
    }
}

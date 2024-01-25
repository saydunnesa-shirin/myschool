using Api.Data.Entities;
using Api.Domain.Countries;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.Countries;

public interface ICountriesRepository
{
    Task<Country> CreateAsync(Country @new, CancellationToken cancellationToken);
    Task<IEnumerable<Country>> GetAllAsync(CancellationToken cancellationToken);
    Task<IEnumerable<Country>> GetListByQueryAsync(CancellationToken cancellationToken);
}

public class CountriesRepository : ICountriesRepository
{
    private readonly ILogger<CountriesRepository> _logger;
    private readonly MySchoolContext _context;

    public CountriesRepository(
      ILogger<CountriesRepository> logger,
      MySchoolContext context)
    {
        _logger = logger;
        _context = context;
    }

    public int Id { get; private set; }

    public async Task<Country> CreateAsync(Country @new, CancellationToken cancellationToken)
    {
        _ = _context.Countries.Add(@new);
        _ = await _context.SaveChangesAsync(cancellationToken);
        return @new;
    }

    public async Task<IEnumerable<Country>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Countries.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Country>> GetListByQueryAsync(CancellationToken cancellationToken)
    {
        return await _context.Countries.ToListAsync(cancellationToken);
    }
}

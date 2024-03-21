using Api.Data.Entities;
using Api.Domain.Countries;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.Countries;

public interface ICountriesRepository
{
    Task<Country> CreateAsync(Country @new, CancellationToken cancellationToken);
    Task<Country> UpdateAsync(Country country, CancellationToken cancellationToken);
    Task<Country> GetAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<Country>> GetAllAsync(CancellationToken cancellationToken);
    Task<IEnumerable<Country>> GetListByQueryAsync(CancellationToken cancellationToken);
    Task<int?> DeleteAsync(int id, CancellationToken cancellationToken);
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
        try
        {
            _ = _context.Countries.Add(@new);
            _ = await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
        
        return @new;
    }

    public async Task<Country> GetAsync(int id, CancellationToken cancellationToken)
    {
        //return await _context.Countries.ToListAsync(cancellationToken);
        var result = await _context.Countries
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        return result;
    }

    public async Task<IEnumerable<Country>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Countries.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Country>> GetListByQueryAsync(CancellationToken cancellationToken)
    {
        return await _context.Countries.ToListAsync(cancellationToken);
    }

    public async Task<Country> UpdateAsync(Country country, CancellationToken cancellationToken)
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
        
        return country;
    }

    public async Task<int?> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            Country deletableCountry = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (deletableCountry != null)
            {
                _ = _context.Countries.Remove(deletableCountry);
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
}

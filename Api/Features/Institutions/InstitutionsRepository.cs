using Api.Data.Entities;
using Api.Domain.Institutions;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.Institutions;

public interface IInstitutionsRepository
{
    Task<Institution> CreateAsync(Institution @new, CancellationToken cancellationToken);
    Task<Institution> UpdateAsync(Institution institution, CancellationToken cancellationToken);

    Task<IEnumerable<Institution>> GetAllAsync(CancellationToken cancellationToken);
    Task<Institution> GetAsync(int id, CancellationToken cancellationToken);

    Task<int?> DeleteAsync(int id, CancellationToken cancellationToken);
}

public class InstitutionsRepository : IInstitutionsRepository
{
    private readonly ILogger<InstitutionsRepository> _logger;
    private readonly MySchoolContext _context;

    public InstitutionsRepository(
      ILogger<InstitutionsRepository> logger,
      MySchoolContext context)
    {
        _logger = logger;
        _context = context;
    }

    public int Id { get; private set; }

    public async Task<Institution> CreateAsync(Institution @new, CancellationToken cancellationToken)
    {
        try
        {
            _ = _context.Institutions.Add(@new);
            _ = await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
        return @new;
    }

    public async Task<IEnumerable<Institution>> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await _context.Institutions
            .Include(x => x.Country)
            .ToListAsync(cancellationToken);

        return result;
    }

    public async Task<Institution> GetAsync(int id, CancellationToken cancellationToken)
    {
        var result = await _context.Institutions
            .Where(x => x.Id == id)
            .Include(x => x.Country)
            .FirstOrDefaultAsync(cancellationToken);

        return result;
    }

    public async Task<Institution> UpdateAsync(Institution institution, CancellationToken cancellationToken)
    {
        try
        {
            _ = _context.Institutions.Update(institution);
            _ = await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
        return institution;
    }

    public async Task<int?> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            Institution deletableInstitution = await _context.Institutions.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (deletableInstitution != null)
            {
                _ = _context.Institutions.Remove(deletableInstitution);
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

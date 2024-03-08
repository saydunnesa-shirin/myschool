using Api.Data.Entities;
using Api.Domain.AcademicSessionTemplates;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.AcademicSessionTemplates;

public interface IAcademicSessionTemplatesRepository
{
    Task<AcademicSessionTemplate> CreateAsync(AcademicSessionTemplate @new, CancellationToken cancellationToken);
    Task<AcademicSessionTemplate> UpdateAsync(AcademicSessionTemplate institution, CancellationToken cancellationToken);
    Task<int?> DeleteAsync(int id, CancellationToken cancellationToken);

    Task<IEnumerable<AcademicSessionTemplate>> GetAllAsync(CancellationToken cancellationToken);
    Task<IEnumerable<AcademicSessionTemplate>> GetListByQueryAsync(CancellationToken cancellationToken);
    Task<IEnumerable<AcademicSessionTemplate>> GetListByInstitutionAsync(int institutionId, CancellationToken cancellationToken);
    Task<AcademicSessionTemplate> GetAsync(int id, CancellationToken cancellationToken);
}

public class AcademicSessionTemplatesRepository : IAcademicSessionTemplatesRepository
{
    private readonly ILogger<AcademicSessionTemplatesRepository> _logger;
    private readonly MySchoolContext _context;

    public AcademicSessionTemplatesRepository(
      ILogger<AcademicSessionTemplatesRepository> logger,
      MySchoolContext context)
    {
        _logger = logger;
        _context = context;
    }

    public int Id { get; private set; }

    public async Task<AcademicSessionTemplate> CreateAsync(AcademicSessionTemplate @new, CancellationToken cancellationToken)
    {
        try
        {
            _ = _context.AcademicSessionTemplates.Add(@new);
            _ = await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
        
        return @new;
    }

    public async Task<AcademicSessionTemplate> UpdateAsync(AcademicSessionTemplate institution, CancellationToken cancellationToken)
    {
        try
        {
            _ = _context.AcademicSessionTemplates.Update(institution);
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
            AcademicSessionTemplate deletableAcademicSessionTemplate = await _context.AcademicSessionTemplates.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (deletableAcademicSessionTemplate != null)
            {
                _ = _context.AcademicSessionTemplates.Remove(deletableAcademicSessionTemplate);
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

    public async Task<IEnumerable<AcademicSessionTemplate>> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await _context.AcademicSessionTemplates
            .Include(x => x.Institution)
            .ToListAsync(cancellationToken);

        return result;
    }

    public async Task<AcademicSessionTemplate> GetAsync(int id, CancellationToken cancellationToken)
    {
        var result = await _context.AcademicSessionTemplates
            .Where(x => x.Id == id)
            .Include(x => x.Institution)
            .FirstOrDefaultAsync(cancellationToken);

        return result;
    }

    public async Task<IEnumerable<AcademicSessionTemplate>> GetListByInstitutionAsync(int institutionId, CancellationToken cancellationToken)
    {
        var result = await _context.AcademicSessionTemplates
            .Where(x => x.InstitutionId == institutionId)
             .Include(x => x.Institution)
             .ToListAsync(cancellationToken);

        return result;
    }

    public async Task<IEnumerable<AcademicSessionTemplate>> GetListByQueryAsync(CancellationToken cancellationToken)
    {
        var result = await _context.AcademicSessionTemplates
            .Include(x => x.Institution)
            .ToListAsync(cancellationToken);

        return result;
    }
}

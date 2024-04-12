using Api.Data.Entities;
using Api.Domain.AcademicClasses;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.AcademicClasses;

public interface IAcademicClassesRepository
{
    Task<AcademicClass> CreateAsync(AcademicClass @new, CancellationToken cancellationToken);
    Task<AcademicClass> UpdateAsync(AcademicClass institution, CancellationToken cancellationToken);

    Task<IEnumerable<AcademicClass>> GetAllAsync(CancellationToken cancellationToken);
    Task<IEnumerable<AcademicClass>> GetListByQueryAsync(AcademicClassQuery query, CancellationToken cancellationToken);
    Task<AcademicClass> GetAsync(int id, CancellationToken cancellationToken);

    Task<int?> DeleteAsync(int id, CancellationToken cancellationToken);
}

public class AcademicClassesRepository : IAcademicClassesRepository
{
    private readonly ILogger<AcademicClassesRepository> _logger;
    private readonly MySchoolContext _context;

    public AcademicClassesRepository(
      ILogger<AcademicClassesRepository> logger,
      MySchoolContext context)
    {
        _logger = logger;
        _context = context;
    }

    public int Id { get; private set; }

    public async Task<AcademicClass> CreateAsync(AcademicClass @new, CancellationToken cancellationToken)
    {
        try
        {
            _ = _context.AcademicClasses.Add(@new);
            _ = await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
        return @new;
    }

    public async Task<IEnumerable<AcademicClass>> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await _context.AcademicClasses
            .Include(x => x.Institution)
            .Include(x => x.AcademicSession)
            .Include(x => x.Teacher)
            .ToListAsync();

        return result;
    }

    public async Task<AcademicClass> GetAsync(int id, CancellationToken cancellationToken)
    {
        var result = await _context.AcademicClasses
            .Where(x => x.Id == id)
            .Include(x => x.Institution)
            .Include(x => x.AcademicSession)
            .Include(x => x.Teacher)
            .FirstOrDefaultAsync(cancellationToken);

        return result;
    }

    public async Task<IEnumerable<AcademicClass>> GetListByQueryAsync(AcademicClassQuery query, CancellationToken cancellationToken)
    {
        var result = await _context.AcademicClasses
            .Where(x => (query.AcademicSessionId == null || x.AcademicSessionId == query.AcademicSessionId) &&
                        (query.InstitutionId == null || x.InstitutionId == query.InstitutionId) &&
                        (query.IsActive == null ? x.IsActive : x.IsActive == query.IsActive))
            .Include(x => x.Institution)
            .Include(x => x.AcademicSession)
            .Include(x => x.Teacher)
            .ToListAsync(cancellationToken);

        return result;
    }

    public async Task<AcademicClass> UpdateAsync(AcademicClass institution, CancellationToken cancellationToken)
    {
        try
        {
            _ = _context.AcademicClasses.Update(institution);
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
            AcademicClass deletableAcademicClass = await _context.AcademicClasses.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (deletableAcademicClass != null)
            {
                _ = _context.AcademicClasses.Remove(deletableAcademicClass);
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

using Api.Data.Entities;
using Api.Domain.AcademicClasses;
using Api.Domain.AcademicSessions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Api.Features.AcademicSessions;

public interface IAcademicSessionsRepository
{
    Task<AcademicSession> CreateAsync(AcademicSession @new, CancellationToken cancellationToken);
    Task<AcademicSession> CreateWithDetailsAsync(AcademicSession @new, IEnumerable<AcademicClass> academicClasses, CancellationToken cancellationToken);
    Task<AcademicSession> UpdateAsync(AcademicSession institution, CancellationToken cancellationToken);

    Task<IEnumerable<AcademicSession>> GetAllAsync(CancellationToken cancellationToken);
    Task<IEnumerable<AcademicSession>> GetListByQueryAsync(CancellationToken cancellationToken);
    Task<IEnumerable<AcademicSession>> GetListByInstitutionAsync(int institutionId, CancellationToken cancellationToken);
    Task<AcademicSession> GetAsync(int id, CancellationToken cancellationToken);

    Task<int?> DeleteAsync(int id, CancellationToken cancellationToken);
}

public class AcademicSessionsRepository : IAcademicSessionsRepository
{
    private readonly ILogger<AcademicSessionsRepository> _logger;
    private readonly MySchoolContext _context;

    public AcademicSessionsRepository(
      ILogger<AcademicSessionsRepository> logger,
      MySchoolContext context)
    {
        _logger = logger;
        _context = context;
    }

    public int Id { get; private set; }

    public async Task<AcademicSession> CreateAsync(AcademicSession @new, CancellationToken cancellationToken)
    {
        try
        {
            _ = _context.AcademicSessions.Add(@new);
            _ = await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
        return @new;
    }

    public async Task<AcademicSession> CreateWithDetailsAsync(AcademicSession @new, IEnumerable<AcademicClass> academicClasses, CancellationToken cancellationToken)
    {
        try
        {
            _ = _context.AcademicSessions.Add(@new);

            foreach (var academicClass in academicClasses)
            {
                academicClass.AcademicSession = @new;
                _ = _context.AcademicClasses.Add(academicClass);
            }
            _ = await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
        return @new;
    }

    public async Task<IEnumerable<AcademicSession>> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await _context.AcademicSessions
            .Include(x => x.AcademicClasses)
            .Include(x => x.Institution)
            .ToListAsync(cancellationToken);

        return result;
    }

    public async Task<AcademicSession> GetAsync(int id, CancellationToken cancellationToken)
    {
        var result = await _context.AcademicSessions
            .Where(x => x.Id == id)
            .Include(x => x.AcademicClasses)
            .Include (x => x.Institution)
            .FirstOrDefaultAsync(cancellationToken);

        return result;

    }

    public async Task<IEnumerable<AcademicSession>> GetListByQueryAsync(CancellationToken cancellationToken)
    {
        var result = await _context.AcademicSessions
            .Include(x => x.AcademicClasses)
            .Include(x => x.Institution)
            .ToListAsync(cancellationToken);

        return result;
    }

    public async Task<IEnumerable<AcademicSession>> GetListByInstitutionAsync(int institutionId, CancellationToken cancellationToken)
    {
        var result = await _context.AcademicSessions
            .Where(x=>x.InstitutionId == institutionId)
            .Include(x => x.AcademicClasses)
            .Include(x => x.Institution)
            .ToListAsync(cancellationToken);

        return result;
    }

    public async Task<AcademicSession> UpdateAsync(AcademicSession institution, CancellationToken cancellationToken)
    {
        try
        {
            _ = _context.AcademicSessions.Update(institution);
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
            AcademicSession deletableAcademicSession = await _context.AcademicSessions.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (deletableAcademicSession != null)
            {
                //_ = _context.AcademicSessions.Remove(deletableAcademicSession);
                deletableAcademicSession.IsActive = false;
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

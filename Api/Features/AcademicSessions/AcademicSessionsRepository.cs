using Api.Data.Entities;
using Api.Domain.AcademicSessions;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.AcademicSessions;

public interface IAcademicSessionsRepository
{
    Task<AcademicSession> CreateAsync(AcademicSession @new, CancellationToken cancellationToken);
    Task<AcademicSession> UpdateAsync(AcademicSession institution, CancellationToken cancellationToken);

    Task<IEnumerable<AcademicSessionViewModel>> GetAllAsync(CancellationToken cancellationToken);
    Task<IEnumerable<AcademicSessionViewModel>> GetListByQueryAsync(CancellationToken cancellationToken);
    Task<IEnumerable<AcademicSessionViewModel>> GetListByInstitutionAsync(int institutionId, CancellationToken cancellationToken);
    Task<AcademicSessionViewModel> GetAsync(int id, CancellationToken cancellationToken);

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

    public async Task<IEnumerable<AcademicSessionViewModel>> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = (
                from a in _context.AcademicSessions
                join i in _context.Institutions on a.InstitutionId equals i.Id
                select new AcademicSessionViewModel
                { 
                    Id = a.Id, 
                    InstitutionId = a.InstitutionId,
                    Name = a.Name,
                    Description = a.Description,
                    StartDate = a.StartDate,
                    EndDate = a.EndDate,
                    InstitutionName = i.Name 
                }).ToListAsync(cancellationToken);

        return await result;
    }

    public async Task<AcademicSessionViewModel> GetAsync(int id, CancellationToken cancellationToken)
    {
        var result = (
                from a in _context.AcademicSessions
                join i in _context.Institutions on a.InstitutionId equals i.Id
                where a.Id == id
                select new AcademicSessionViewModel
                {
                    Id = a.Id,
                    InstitutionId = a.InstitutionId,
                    Name = a.Name,
                    Description = a.Description,
                    StartDate = a.StartDate,
                    EndDate = a.EndDate,
                    InstitutionName = i.Name
                }).FirstOrDefaultAsync(cancellationToken);

        return await result;
    }

    public async Task<IEnumerable<AcademicSessionViewModel>> GetListByQueryAsync(CancellationToken cancellationToken)
    {
        var result = (
                from a in _context.AcademicSessions
                join i in _context.Institutions on a.InstitutionId equals i.Id
                orderby a.StartDate descending

                select new AcademicSessionViewModel
                {
                    Id = a.Id,
                    InstitutionId = a.InstitutionId,
                    Name = a.Name,
                    Description = a.Description,
                    StartDate = a.StartDate,
                    EndDate = a.EndDate,
                    InstitutionName = i.Name
                }
                ).ToListAsync(cancellationToken);

        return await result;
    }

    public async Task<IEnumerable<AcademicSessionViewModel>> GetListByInstitutionAsync(int institutionId, CancellationToken cancellationToken)
    {
        var result = (
                from a in _context.AcademicSessions
                join i in _context.Institutions on a.InstitutionId equals i.Id
                where a.InstitutionId == institutionId
                orderby a.StartDate descending
                select new AcademicSessionViewModel
                {
                    Id = a.Id,
                    InstitutionId = a.InstitutionId,
                    Name = a.Name,
                    Description = a.Description,
                    StartDate = a.StartDate,
                    EndDate = a.EndDate,
                    InstitutionName = i.Name
                }
                ).ToListAsync(cancellationToken);

        return await result;
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
                _ = _context.AcademicSessions.Remove(deletableAcademicSession);
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

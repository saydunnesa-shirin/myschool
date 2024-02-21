using Api.Data.Entities;
using Api.Domain.AcademicClasses;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.AcademicClasses;

public interface IAcademicClassesRepository
{
    Task<AcademicClass> CreateAsync(AcademicClass @new, CancellationToken cancellationToken);
    Task<AcademicClass> UpdateAsync(AcademicClass institution, CancellationToken cancellationToken);

    Task<IEnumerable<AcademicClassViewModel>> GetAllAsync(CancellationToken cancellationToken);
    Task<IEnumerable<AcademicClassViewModel>> GetListByQueryAsync(CancellationToken cancellationToken);
    Task<AcademicClassViewModel> GetAsync(int id, CancellationToken cancellationToken);

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

    public async Task<IEnumerable<AcademicClassViewModel>> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = (
                from ac in _context.AcademicClasses
                join i in _context.Institutions on ac.InstitutionId equals i.Id
                join s in _context.AcademicSessions on ac.AcademicSessionId equals s.Id
                join e in _context.Employees on ac.TeacherId equals e.Id

                select new AcademicClassViewModel
                { 
                    Id = ac.Id, 
                    InstitutionId = ac.InstitutionId,
                    AcademicSessionId = ac.AcademicSessionId,
                    TeacherId = ac.TeacherId,
                    Name = ac.Name,
                    InstitutionName = i.Name,
                    AcademicSessionName = s.Name,
                    TeacherName = e.FirstName
                }).ToListAsync(cancellationToken);

        return await result;
    }

    public async Task<AcademicClassViewModel> GetAsync(int id, CancellationToken cancellationToken)
    {
        var result = (
                from ac in _context.AcademicClasses
                join i in _context.Institutions on ac.InstitutionId equals i.Id
                join s in _context.AcademicSessions on ac.AcademicSessionId equals s.Id
                join e in _context.Employees on ac.TeacherId equals e.Id

                where ac.Id == id
                select new AcademicClassViewModel
                {
                    Id = ac.Id,
                    InstitutionId = ac.InstitutionId,
                    AcademicSessionId = ac.AcademicSessionId,
                    TeacherId = ac.TeacherId,
                    Name = ac.Name,
                    InstitutionName = i.Name,
                    AcademicSessionName = s.Name,
                    TeacherName = e.FirstName
                }).FirstOrDefaultAsync(cancellationToken);

        return await result;
    }

    public async Task<IEnumerable<AcademicClassViewModel>> GetListByQueryAsync(CancellationToken cancellationToken)
    {
        var result = (
                from ac in _context.AcademicClasses
                join i in _context.Institutions on ac.InstitutionId equals i.Id
                join s in _context.AcademicSessions on ac.AcademicSessionId equals s.Id
                join e in _context.Employees on ac.TeacherId equals e.Id
                select new AcademicClassViewModel
                {
                    Id = ac.Id,
                    InstitutionId = ac.InstitutionId,
                    AcademicSessionId = ac.AcademicSessionId,
                    TeacherId = ac.TeacherId,
                    Name = ac.Name,
                    InstitutionName = i.Name,
                    AcademicSessionName = s.Name,
                    TeacherName = e.FirstName
                }).ToListAsync(cancellationToken);

        return await result;
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

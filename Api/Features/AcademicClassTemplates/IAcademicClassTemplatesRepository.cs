using Api.Data.Entities;
using Api.Domain.AcademicClassTemplates;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.AcademicClassTemplates;

public interface IAcademicClassTemplatesRepository
{
    Task<AcademicClassTemplate> CreateAsync(AcademicClassTemplate @new, CancellationToken cancellationToken);
    Task<AcademicClassTemplate> UpdateAsync(AcademicClassTemplate institution, CancellationToken cancellationToken);
    Task<int?> DeleteAsync(int id, CancellationToken cancellationToken);

    Task<IEnumerable<AcademicClassTemplate>> GetAllAsync(CancellationToken cancellationToken);
    Task<IEnumerable<AcademicClassTemplate>> GetListByQueryAsync(AcademicClassTemplateQuery query, CancellationToken cancellationToken);
    Task<AcademicClassTemplate> GetAsync(int id, CancellationToken cancellationToken);
}

public class AcademicClassTemplatesRepository : IAcademicClassTemplatesRepository
{
    private readonly ILogger<AcademicClassTemplatesRepository> _logger;
    private readonly MySchoolContext _context;

    public AcademicClassTemplatesRepository(
      ILogger<AcademicClassTemplatesRepository> logger,
      MySchoolContext context)
    {
        _logger = logger;
        _context = context;
    }

    public int Id { get; private set; }

    public async Task<AcademicClassTemplate> CreateAsync(AcademicClassTemplate @new, CancellationToken cancellationToken)
    {
        try
        {
            _ = _context.AcademicClassTemplates.Add(@new);
            _ = await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
        
        return @new;
    }

    public async Task<AcademicClassTemplate> UpdateAsync(AcademicClassTemplate institution, CancellationToken cancellationToken)
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
        return institution;
    }

    public async Task<int?> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            AcademicClassTemplate deletableAcademicClassTemplate = await _context.AcademicClassTemplates.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (deletableAcademicClassTemplate != null)
            {
                _ = _context.AcademicClassTemplates.Remove(deletableAcademicClassTemplate);
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

    public async Task<IEnumerable<AcademicClassTemplate>> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await _context.AcademicClassTemplates
            .Include(x => x.Institution)
            .OrderBy(x => x.SerialNo)
            .ToListAsync(cancellationToken);

        return result;
    }

    public async Task<AcademicClassTemplate> GetAsync(int id, CancellationToken cancellationToken)
    {
        var result = await _context.AcademicClassTemplates
            .Where(x => x.Id == id)
            .Include(x => x.Institution)
            .FirstOrDefaultAsync(cancellationToken);

        return result;
    }

    public async Task<IEnumerable<AcademicClassTemplate>> GetListByQueryAsync(AcademicClassTemplateQuery query, CancellationToken cancellationToken)
    {
        var result = await _context.AcademicClassTemplates
            .Where(x => (query.IsActive == null ? x.IsActive : x.IsActive == query.IsActive) &&
                    (query.InstitutionId == null || x.InstitutionId == query.InstitutionId))
            .Include(x => x.Institution)
            .OrderBy(x => x.SerialNo)
            .ToListAsync(cancellationToken);

        return result;
    }
}

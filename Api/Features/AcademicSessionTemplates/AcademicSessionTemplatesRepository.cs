using Api.Data.Entities;
using Api.Domain.AcademicSessionTemplates;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.AcademicSessionTemplates;

public interface IAcademicSessionTemplatesRepository
{
    Task<AcademicSessionTemplate> CreateAsync(AcademicSessionTemplate @new, CancellationToken cancellationToken);
    Task<AcademicSessionTemplate> UpdateAsync(AcademicSessionTemplate institution, CancellationToken cancellationToken);

    Task<IEnumerable<AcademicSessionTemplateViewModel>> GetAllAsync(CancellationToken cancellationToken);
    Task<IEnumerable<AcademicSessionTemplateViewModel>> GetListByQueryAsync(CancellationToken cancellationToken);
    Task<IEnumerable<AcademicSessionTemplateViewModel>> GetListByInstitutionAsync(int institutionId, CancellationToken cancellationToken);

    Task<AcademicSessionTemplateViewModel> GetAsync(int id, CancellationToken cancellationToken);

    Task<int?> DeleteAsync(int id, CancellationToken cancellationToken);
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

    public async Task<IEnumerable<AcademicSessionTemplateViewModel>> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = (
                from st in _context.AcademicSessionTemplates
                join i in _context.Institutions on st.InstitutionId equals i.Id
                select new AcademicSessionTemplateViewModel
                { 
                    Id = st.Id, 
                    InstitutionId = st.InstitutionId,
                    TemplateName = st.TemplateName,
                    InstitutionName = i.Name
                }).ToListAsync(cancellationToken);

        return await result;
    }

    public async Task<AcademicSessionTemplateViewModel> GetAsync(int id, CancellationToken cancellationToken)
    {
        var result = (
                from st in _context.AcademicSessionTemplates
                join i in _context.Institutions on st.InstitutionId equals i.Id
                where st.Id == id
                select new AcademicSessionTemplateViewModel
                {
                    Id = st.Id,
                    InstitutionId = st.InstitutionId,
                    TemplateName = st.TemplateName,
                    InstitutionName = i.Name
                }).FirstOrDefaultAsync(cancellationToken);

        return await result;
    }

    public async Task<IEnumerable<AcademicSessionTemplateViewModel>> GetListByInstitutionAsync(int institutionId, CancellationToken cancellationToken)
    {
        var result = (
                from st in _context.AcademicSessionTemplates
                join i in _context.Institutions on st.InstitutionId equals i.Id
                where st.InstitutionId == institutionId
                select new AcademicSessionTemplateViewModel
                {
                    Id = st.Id,
                    InstitutionId = st.InstitutionId,
                    TemplateName = st.TemplateName,
                    InstitutionName = i.Name
                }).ToListAsync(cancellationToken);

        return await result;
    }

    public async Task<IEnumerable<AcademicSessionTemplateViewModel>> GetListByQueryAsync(CancellationToken cancellationToken)
    {
        var result = (
                from st in _context.AcademicSessionTemplates
                join i in _context.Institutions on st.InstitutionId equals i.Id
                select new AcademicSessionTemplateViewModel
                {
                    Id = st.Id,
                    InstitutionId = st.InstitutionId,
                    TemplateName = st.TemplateName,
                    InstitutionName = i.Name
                }
                ).ToListAsync(cancellationToken);

        return await result;
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
}

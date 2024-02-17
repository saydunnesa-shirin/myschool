using Api.Data.Entities;
using Api.Domain.Employees;
using Api.Domain.Institutions;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.Institutions;

public interface IInstitutionsRepository
{
    Task<Institution> CreateAsync(Institution @new, CancellationToken cancellationToken);
    Task<Institution> UpdateAsync(Institution institution, CancellationToken cancellationToken);

    Task<IEnumerable<InstitutionViewModel>> GetAllAsync(CancellationToken cancellationToken);
    Task<IEnumerable<InstitutionViewModel>> GetListByQueryAsync(CancellationToken cancellationToken);
    Task<InstitutionViewModel> GetAsync(int id, CancellationToken cancellationToken);

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
        _ = _context.Institutions.Add(@new);
        _ = await _context.SaveChangesAsync(cancellationToken);
        return @new;
    }

    public async Task<IEnumerable<InstitutionViewModel>> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = (
                from i in _context.Institutions
                join c in _context.Countries on i.CountryId equals c.Id
                select new InstitutionViewModel
                { 
                    Id = i.Id, 
                    CountryId = i.CountryId,
                    Name = i.Name,
                    Address = i.Address,
                    CountryName = c.Name }
                ).ToListAsync(cancellationToken);

        return await result;
    }

    public async Task<InstitutionViewModel> GetAsync(int id, CancellationToken cancellationToken)
    {
        var result = (
                from i in _context.Institutions
                join c in _context.Countries on i.CountryId equals c.Id
                where i.Id == id
                select new InstitutionViewModel
                {
                    Id = i.Id,
                    CountryId = i.CountryId,
                    Name = i.Name,
                    Address = i.Address,
                    CountryName = c.Name
                }
                ).FirstOrDefaultAsync(cancellationToken);

        return await result;
    }

    public async Task<IEnumerable<InstitutionViewModel>> GetListByQueryAsync(CancellationToken cancellationToken)
    {
        var result = (
                from i in _context.Institutions
                join c in _context.Countries on i.CountryId equals c.Id
                select new InstitutionViewModel
                {
                    Id = i.Id,
                    CountryId = i.CountryId,
                    Name = i.Name,
                    Address = i.Address,
                    CountryName = c.Name
                }
                ).ToListAsync(cancellationToken);

        return await result;
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
        Institution deletableInstitution = await _context.Institutions.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (deletableInstitution != null)
        {
            _ = _context.Institutions.Remove(deletableInstitution);
            _ = await _context.SaveChangesAsync(cancellationToken);
            return id;
        }
        return null;
    }
}

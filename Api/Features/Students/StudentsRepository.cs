using Api.Data.Entities;
using Api.Domain.Students;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.Students;

public interface IStudentsRepository
{
    Task<Student> CreateAsync(Student newStudent, CancellationToken cancellationToken);
    Task<Student> UpdateAsync(Student student, CancellationToken cancellationToken);
    Task<int?> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<Student>> GetListByQueryAsync(StudentQuery query, CancellationToken cancellationToken);
    Task<Student> GetByIdyAsync(int id, CancellationToken cancellationToken);
    Task<Student> GetByEmailAsync(string email, CancellationToken cancellationToken);
}

public class StudentsRepository : IStudentsRepository
{
    private readonly ILogger<StudentsRepository> _logger;
    private readonly MySchoolContext _context;

    public StudentsRepository(
      ILogger<StudentsRepository> logger,
      MySchoolContext context)
    {
        ArgumentNullException.ThrowIfNull(logger);

        _logger = logger;
        _context = context;
    }

    public int Id { get; private set; }

    public async Task<Student> CreateAsync(Student newStudent, CancellationToken cancellationToken)
    {
        try
        {
            _ = _context.Students.Add(newStudent);
            _ = await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
        return newStudent;
    }

    public async Task<Student> UpdateAsync(Student student, CancellationToken cancellationToken)
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
        return student;
    }

    public async Task<int?> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            Student deletableStudent = await _context.Students.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (deletableStudent != null)
            {
                _ = _context.Students.Remove(deletableStudent);
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

    public async Task<IEnumerable<Student>> GetListByQueryAsync(StudentQuery query, CancellationToken cancellationToken)
    {
        var result = await _context.Students
            .Where(x => (query.IsActive == null ? x.IsActive : x.IsActive == query.IsActive) &&
                        (query.Ids == null || query.Ids.Contains(x.Id)) &&
                        (string.IsNullOrEmpty(query.FirstName) || x.FirstName.ToLower().Contains(query.FirstName.ToLower())) &&
                        (query.StatusIds == null || query.StatusIds.Contains(x.StatusId)) &&
                        (query.StatusReasonIds == null || query.StatusReasonIds.Contains(x.StatusReasonId)) &&
                        (query.InstitutionId == null || x.InstitutionId == query.InstitutionId) &&

                        (query.ActiveSessionIds == null || query.ActiveSessionIds.Contains(x.ActiveSessionId)) &&
                        (query.ActiveClassIds == null || query.ActiveClassIds.Contains(x.ActiveClassId)))
            .Include(x => x.Institution)
            .Include(x => x.Country)
            .Include(x => x.AcademicSession)
            .Include(x => x.AcademicClass)
            .ToListAsync(cancellationToken);

        return result;
    }

    public async Task<Student> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var result = await _context.Students
            .Where(x => x.Email == email)
            .Include(x => x.Institution)
            .Include(x => x.Country)
            .Include(x => x.AcademicSession)
            .Include(x => x.AcademicClass)
            .FirstOrDefaultAsync(cancellationToken);

        return result;
    }

    public async Task<Student> GetByIdyAsync(int id, CancellationToken cancellationToken)
    {
        var result = await _context.Students
            .Where(x => x.Id == id)
            .Include(x => x.Institution)
            .Include(x => x.Country)
            .Include(x => x.AcademicSession)
            .Include(x => x.AcademicClass)
            .FirstOrDefaultAsync(cancellationToken);

        return result;
    }
}

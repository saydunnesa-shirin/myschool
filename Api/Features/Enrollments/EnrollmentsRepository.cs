using Api.Common;
using Api.Data.Entities;
using Api.Domain.AcademicClasses;
using Api.Domain.Enrollments;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.Enrollments;

public interface IEnrollmentsRepository
{
    Task<Enrollment> CreateAsync(Enrollment newEnrollment, CancellationToken cancellationToken);
    Task<IEnumerable<Enrollment>> CreateAsync(IEnumerable<Enrollment> newEnrollments, CancellationToken cancellationToken);
    Task<Enrollment> UpdateAsync(Enrollment enrollment, CancellationToken cancellationToken);
    Task<Enrollment> UpdateWithStudentStatusAsync(Enrollment enrollment, CancellationToken cancellationToken, int studentStatusId, int studentStatusReasonId);
    Task<int?> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<Enrollment>> GetListByQueryAsync(EnrollmentQuery query, CancellationToken cancellationToken);
    Task<Enrollment> GetByIdyAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<Enrollment>> GetListByIdsyAsync(EnrollmentQuery query, CancellationToken cancellationToken);
}

public class EnrollmentsRepository : IEnrollmentsRepository
{
    private readonly ILogger<EnrollmentsRepository> _logger;
    private readonly MySchoolContext _context;

    public EnrollmentsRepository(
      ILogger<EnrollmentsRepository> logger,
      MySchoolContext context)
    {
        ArgumentNullException.ThrowIfNull(logger);

        _logger = logger;
        _context = context;
    }

    public int Id { get; private set; }

    public async Task<Enrollment> CreateAsync(Enrollment newEnrollment, CancellationToken cancellationToken)
    {
        try
        {
            _ = _context.Enrollments.Add(newEnrollment);
            _ = await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
        return newEnrollment;
    }

    public async Task<IEnumerable<Enrollment>> CreateAsync(IEnumerable<Enrollment> newEnrollments, CancellationToken cancellationToken)
    {
        try
        {
            var studentIds = newEnrollments.Select(e => e.StudentId).ToList();
            var students = _context.Students.Where(s => studentIds.Contains(s.Id)).ToList();

            students = students.Select(item =>
            {
                item.StatusId = (int)StudentStatus.Enrolled;
                item.UpdatedBy = 1;
                item.UpdatedDate = DateTime.Now;
                return item;
            }).ToList();

            foreach (var newEnrollment in newEnrollments)
            {
                _ = _context.Enrollments.Add(newEnrollment);
                
            }
            _ = await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
        return newEnrollments;
    }

    public async Task<Enrollment> UpdateAsync(Enrollment enrollment, CancellationToken cancellationToken)
    {
        try
        {
            var student = _context.Students.Where(s => s.Id == enrollment.StudentId).FirstOrDefault();

            if (student != null && (student.ActiveSessionId != enrollment.AcademicSessionId  || student.ActiveClassId != enrollment.AcademicClassId))
            {
                student.ActiveSessionId = enrollment.AcademicSessionId;
                student.ActiveClassId = enrollment.AcademicClassId;
                student.UpdatedBy = 1;
                student.UpdatedDate = DateTime.Now;
            }

            _ = _context.Enrollments.Update(enrollment);
            _ = await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
        return enrollment;
    }

    public async Task<Enrollment> UpdateWithStudentStatusAsync(Enrollment enrollment, CancellationToken cancellationToken, int studentStatusId, int studentStatusReasonId)
    {
        try
        {
            var student = _context.Students.Where(s => s.Id == enrollment.StudentId).FirstOrDefault();

            if (student != null && (student.StatusId != studentStatusId || student.StatusReasonId != studentStatusReasonId))
            {
                student.StatusId = studentStatusId;
                student.StatusReasonId = studentStatusReasonId;
                student.UpdatedBy = 1;
                student.UpdatedDate = DateTime.Now;
            }

            _ = _context.Enrollments.Update(enrollment);
            _ = await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
        return enrollment;
    }

    public async Task<int?> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            Enrollment deletableEnrollment = await _context.Enrollments.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (deletableEnrollment != null)
            {
                _ = _context.Enrollments.Remove(deletableEnrollment);
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

    public async Task<IEnumerable<Enrollment>> GetListByQueryAsync(EnrollmentQuery query, CancellationToken cancellationToken)
    {
        var result = await _context.Enrollments
            .Where(x => (query.IsActive == null ? x.IsActive : x.IsActive == query.IsActive) &&
                        x.Student.StatusId == (int)StudentStatus.Enrolled &&
                        (query.Ids == null || query.Ids.Contains(x.Id)) &&
                        (string.IsNullOrEmpty(query.StudentName) || x.Student.FirstName.ToLower().Contains(query.StudentName.ToLower())) &&
                        (query.StudentIds == null || query.StudentIds.Contains(x.StudentId)) &&
                        (query.InstitutionIds == null || query.InstitutionIds.Contains(x.InstitutionId)) &&
                        (query.AcademicSessionIds == null || query.AcademicSessionIds.Contains(x.AcademicSessionId)) &&
                        (query.AcademicClassIds == null || query.AcademicClassIds.Contains(x.AcademicClassId)))
            .Include(x => x.Institution)
            .Include(x => x.Student)
            .Include(x => x.AcademicSession)
            .Include(x => x.AcademicClass)
            .ToListAsync(cancellationToken);

        return result;
    }

    public async Task<Enrollment> GetByIdyAsync(int id, CancellationToken cancellationToken)
    {
        var result = await _context.Enrollments
            .Where(x => x.Id == id)
            .Include(x => x.Institution)
            .Include(x => x.Student)
            .Include(x => x.AcademicSession)
            .Include(x => x.AcademicClass)
            .FirstOrDefaultAsync(cancellationToken);

        return result;
    }

    public async Task<IEnumerable<Enrollment>> GetListByIdsyAsync(EnrollmentQuery query, CancellationToken cancellationToken)
    {
        var result = await _context.Enrollments
            .Where(x => (query.Ids == null || query.Ids.Contains(x.Id)) &&
                        (query.StudentIds == null || query.StudentIds.Contains(x.StudentId)))
            .Include(x => x.Institution)
            .Include(x => x.Student)
            .Include(x => x.AcademicSession)
            .Include(x => x.AcademicClass)
            .ToListAsync(cancellationToken);

        return result;
    }
}

using Api.Data.Entities;
using Api.Domain.AcademicClasses;
using Api.Domain.AcademicSessions;
using Api.Domain.Institutions;
using Api.Domain.Students;

namespace Api.Domain.Enrollments;

public class Enrollment : BaseEntity
{
    public int InstitutionId { get; set; }
    public DateTime EnrollmentDate { get; set; }

    // Academic Information
    public int AcademicSessionId { get; set; }
    public int AcademicClassId { get; set; }
    public int StudentId { get; set; }

    public virtual Institution Institution { get; set; }
    public virtual Student Student { get; set; }
    public virtual AcademicSession AcademicSession { get; set; }
    public virtual AcademicClass AcademicClass { get; set; }
}

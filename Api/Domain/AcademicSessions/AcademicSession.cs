using Api.Data.Entities;
using Api.Domain.AcademicClasses;
using Api.Domain.Enrollments;
using Api.Domain.Institutions;
using Api.Domain.Students;

namespace Api.Domain.AcademicSessions
{
    public class AcademicSession : BaseEntity
    {
        public int InstitutionId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual Institution Institution { get; set; }
        public virtual ICollection<AcademicClass> AcademicClasses { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}

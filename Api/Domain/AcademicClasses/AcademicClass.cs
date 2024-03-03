using Api.Data.Entities;
using Api.Domain.AcademicSessions;
using Api.Domain.Employees;
using Api.Domain.Institutions;

namespace Api.Domain.AcademicClasses
{
    public class AcademicClass: BaseEntity
    {
        public int InstitutionId { get; set; }
        public int AcademicSessionId { get; set; }
        public int? TeacherId { get; set; }
        public string Name { get; set; }

        public virtual Institution Institution { get; set; }

        public virtual AcademicSession AcademicSession { get; set; }

        //[ForeignKey("TeacherId")]
        public virtual Employee Teacher { get; set; }
    }
}

using Api.Data.Entities;
using Api.Domain.Institutions;
using Api.Domain.AcademicSessions;
using Api.Domain.Employees;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Domain.AcademicClasses
{
    public class AcademicClass: BaseEntity
    {
        public int InstitutionId { get; set; }
        public int AcademicSessionId { get; set; }
        public int? TeacherId { get; set; }
        public string Name { get; set; }

        public virtual Institution Institutions { get; set; }

        public virtual AcademicSession AcademicSessions { get; set; }

        [ForeignKey("TeacherId")]
        public virtual Employee Teachers { get; set; }
    }
}

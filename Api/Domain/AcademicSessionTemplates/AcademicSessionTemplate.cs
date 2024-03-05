using Api.Data.Entities;
using Api.Domain.Institutions;

namespace Api.Domain.AcademicSessionTemplates
{
    public class AcademicSessionTemplate : BaseEntity
    {
        public string TemplateName { get; set; }
        public int InstitutionId { get; set; }

        public virtual Institution Institution { get; set; }
    }
}

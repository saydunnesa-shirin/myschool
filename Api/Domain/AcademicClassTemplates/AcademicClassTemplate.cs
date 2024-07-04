using Api.Data.Entities;
using Api.Domain.Institutions;

namespace Api.Domain.AcademicClassTemplates
{
    public class AcademicClassTemplate : BaseEntity
    {
        public string TemplateName { get; set; }
        public int InstitutionId { get; set; }
        public int SerialNo { get; set; }

        public virtual Institution Institution { get; set; }
    }
}

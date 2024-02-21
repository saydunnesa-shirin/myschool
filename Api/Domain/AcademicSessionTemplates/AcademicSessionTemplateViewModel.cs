namespace Api.Domain.AcademicSessionTemplates
{
    public class AcademicSessionTemplateViewModel
    {
        public int Id { get; set; }
        public string TemplateName { get; set; }
        public int InstitutionId { get; set; }
        public string InstitutionName { get; set; } = string.Empty;
    }
}

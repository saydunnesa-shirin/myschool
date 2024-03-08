namespace Api.Features.AcademicSessionTemplates;

public record AcademicSessionTemplateResult
{
    public int Id { get; set; }
    public string TemplateName { get; set; }
    public int InstitutionId { get; set; }
    public string InstitutionName { get; set; } = string.Empty;
}

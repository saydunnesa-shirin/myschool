namespace Api.Features.AcademicClassTemplates;

public record AcademicClassTemplateResult
{
    public int Id { get; set; }
    public string TemplateName { get; set; }
    public int InstitutionId { get; set; }
    public string InstitutionName { get; set; } = string.Empty;
    public int SerialNo { get; set; }
}

namespace Api.Features.AcademicClasses;

public record BaseResult
{
    public int Id { get; set; }
    public int InstitutionId { get; set; }
    public int AcademicSessionId { get; set; }
    public int? TeacherId { get; set; }
    public string Name { get; set; }
}

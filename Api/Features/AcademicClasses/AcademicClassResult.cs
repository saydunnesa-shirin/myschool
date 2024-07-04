namespace Api.Features.AcademicClasses;

public record AcademicClassResult
{
    public int Id { get; set; }
    public int InstitutionId { get; set; }
    public int AcademicSessionId { get; set; }
    public int? TeacherId { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public string TeacherName { get; set; }
    public int? TemplateId { get; set; }
}

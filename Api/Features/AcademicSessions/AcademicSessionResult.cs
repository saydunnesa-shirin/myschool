using Api.Domain.AcademicClasses;
using Api.Features.AcademicClasses;

namespace Api.Features.AcademicSessions;

public record AcademicSessionResult
{
    public int Id { get; set; }
    public int InstitutionId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string InstitutionName { get; set; } = string.Empty;
    public ICollection<AcademicClassResult> AcademicClasses { get; set; }
}

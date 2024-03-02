using Api.Domain.AcademicClasses;

namespace Api.Domain.AcademicSessions
{
    public class AcademicSessionViewModel
    {
        public int Id { get; set; }
        public int InstitutionId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string InstitutionName { get; set; } = string.Empty;

        public IEnumerable<AcademicClassViewModel> AcademicClasses = null;
    }
}
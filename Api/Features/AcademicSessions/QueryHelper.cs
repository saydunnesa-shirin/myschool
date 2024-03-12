namespace Api.Features.AcademicSessions
{
    public class QueryHelper
    {
    }

    public class AcademicSessionQuery
    {
        public int? Id { get; set; }
        public int? InstitutionId { get; set; }
        public string Term { get; set; } = string.Empty;
        public bool? IsActive { get; set; }
    }
}

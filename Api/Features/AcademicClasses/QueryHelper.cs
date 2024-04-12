namespace Api.Features.AcademicClasses
{
    public class QueryHelper
    {
    }

    public class AcademicClassQuery
    {
        public bool? IsActive { get; set; }
        public int? InstitutionId { get; set; }
        public int? AcademicSessionId { get; set; }
    }
}

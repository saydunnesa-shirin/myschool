namespace Api.Features.Students
{
    public class QueryHelper
    {
    }

    public class StudentQuery
    {
        public bool? IsActive { get; set; }
        public int? InstitutionId { get; set; }
        public int? ActiveSessionId { get; set; }
        public int? ActiveClassId { get; set; }
        public int? StatusId { get; set; }
        public int? StatusReasonId { get; set; }
    }
}

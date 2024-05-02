namespace Api.Features.Students
{
    public class QueryHelper
    {
    }

    public class StudentQuery
    {
        public List<int> Ids { get; set; }
        public int? InstitutionId { get; set; }
        public List<int> ActiveSessionIds { get; set; }
        public List<int> ActiveClassIds { get; set; }
        public List<int> StatusIds { get; set; }
        public List<int?> StatusReasonIds { get; set; }
        public bool? IsActive { get; set; } = true;
        public string? FirstName { get; set; }
    }
}

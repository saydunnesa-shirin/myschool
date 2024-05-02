namespace Api.Features.Enrollments
{
    public class QueryHelper
    {
    }

    public class EnrollmentQuery
    {
        public bool? IsActive { get; set; }

        public List<int> Ids { get; set; }
        public List<int> StudentIds { get; set; }
        public List<int> InstitutionIds { get; set; }
        public List<int> AcademicSessionIds { get; set; }
        public List<int> AcademicClassIds { get; set; }
    }
}

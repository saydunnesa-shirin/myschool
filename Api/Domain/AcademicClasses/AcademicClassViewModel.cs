namespace Api.Domain.AcademicClasses
{
    public class AcademicClassViewModel
    {
        public int Id { get; set; }
        public int InstitutionId { get; set; }
        public int AcademicSessionId { get; set; }
        public int? TeacherId { get; set; }
        public string Name { get; set; }
        public string InstitutionName { get; set; } = string.Empty;
        public string AcademicSessionName { get; set; } = string.Empty;
        public string TeacherName { get; set; } = string.Empty;
    }
}

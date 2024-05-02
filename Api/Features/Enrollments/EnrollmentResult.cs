namespace Api.Features.Enrollments;

public record EnrollmentResult
{
    public int Id { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public int InstitutionId { get; set; }
    public int AcademicSessionId { get; set; }
    public int AcademicClassId { get; set; }
    public int StudentId { get; set; }

    public string AcademicSessionName { get; set; }
    public string AcademicClassName { get; set; }
    public string StudentName { get; set; }

    public DateTime CreatedDate { get; set; }
    public int CreatedBy { get; set; }
    public DateTime UpdatedDate { get; set; }
    public int UpdatedBy { get; set; }
}

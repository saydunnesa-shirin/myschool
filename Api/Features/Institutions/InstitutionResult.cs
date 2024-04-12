namespace Api.Features.Institutions;

public record InstitutionResult
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int CountryId { get; set; }
    public bool IsActive { get; set; }
    public string Address { get; set; }
    public string CountryName { get; set; } = string.Empty;
}

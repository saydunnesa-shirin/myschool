namespace Api.Features.Institutions;

public record BaseResult
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int CountryId { get; set; }
    public string Address { get; set; }
    public string CountryName { get; set; } = string.Empty;
}

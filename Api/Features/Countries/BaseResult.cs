namespace Api.Features.Countries;

public record BaseResult
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Iso2Code { get; set; } = string.Empty;
}

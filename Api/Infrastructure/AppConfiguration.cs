namespace Api.Infrastructure;

public class AppConfiguration
{
    public bool IsStackTraceAllowed { get; init; }
    public string ApiKeyName { get; init; } = string.Empty;
    public string[] ApiKeyIgnorePaths { get; init; } = Array.Empty<string>();
}

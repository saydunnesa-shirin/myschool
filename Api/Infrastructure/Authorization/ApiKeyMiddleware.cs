using Microsoft.Extensions.Options;

namespace Api.Infrastructure.Authorization;

public class ApiKeyMiddleware
{
  private readonly string _apiKey;
  private readonly string _apiKeyName;
  private readonly RequestDelegate _next;
  private readonly string[] _pathsToIgnore;

  public ApiKeyMiddleware(RequestDelegate next,
    IOptions<AppConfiguration> appConfiguration,
    IOptions<VaultSecretsConfiguration> vaultSecretsConfiguration)
  {
    _next = next;
    _apiKey = vaultSecretsConfiguration.Value.MY_SCHOOL_SERVICE_API_KEY;
    _apiKeyName = appConfiguration.Value.ApiKeyName;
    _pathsToIgnore = appConfiguration.Value.ApiKeyIgnorePaths;
  }

  public async Task InvokeAsync(HttpContext context)
  {
    if (context.Request.Path.Value != null &&
        _pathsToIgnore.Any(context.Request.Path.Value.Contains))
    {
      await _next(context);
      return;
    }

    //if (!context.Request.Headers.TryGetValue(_apiKeyName, out var extractedApiKey))
    //  throw new UnauthorizedAccessException("Api Key was not provided");

    //if (!_apiKey.Equals(extractedApiKey))
    //  throw new UnauthorizedAccessException("Unauthorized client");

    await _next(context);
  }
}

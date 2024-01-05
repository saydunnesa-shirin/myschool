using System.Text.Json;
using Microsoft.Extensions.Options;

namespace Api.Infrastructure.Exceptions;

public class GlobalErrorHandlingMiddleware
{
  private readonly AppConfiguration _appConfiguration;
  private readonly ILogger _logger;
  private readonly RequestDelegate _next;

  public GlobalErrorHandlingMiddleware(
    RequestDelegate next,
    ILogger<GlobalErrorHandlingMiddleware> logger,
    IOptions<AppConfiguration> appConfiguration)
  {
    _next = next;
    _logger = logger;
    _appConfiguration = appConfiguration.Value;
  }

  public async Task Invoke(HttpContext context)
  {
    try
    {
      await _next(context);
    }
    catch (Exception ex)
    {
      await HandleExceptionAsync(context, ex);
    }
  }

  private Task HandleExceptionAsync(HttpContext context, Exception exception)
  {
    var statusCode = (int)ApiStatusCodeHelper.FromException(exception);
    var message = exception.Message;
    var stackTrace = exception.StackTrace;

    _logger.LogError("Exception message: {Message} Statuscode: {StatusCode}", message, statusCode);

    string exceptionResult;
    if (_appConfiguration.IsStackTraceAllowed)
      exceptionResult = JsonSerializer.Serialize(new ApiError
        { Code = statusCode, Message = message, Cause = stackTrace });
    else
      exceptionResult =
        JsonSerializer.Serialize(new ApiError { Code = statusCode, Message = message });
    context.Response.ContentType = "application/json";
    context.Response.StatusCode = statusCode;

    return context.Response.WriteAsync(exceptionResult);
  }
}

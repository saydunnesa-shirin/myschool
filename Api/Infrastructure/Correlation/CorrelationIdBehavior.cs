using Microsoft.Extensions.Primitives;

namespace Api.Infrastructure.Correlation;

public class CorrelationIdBehavior
{
  private const string _correlationIdHeader = "X-Request-Id";
  private readonly RequestDelegate _next;

  public CorrelationIdBehavior(RequestDelegate next)
  {
    _next = next;
  }

  public async Task Invoke(HttpContext context, ICorrelationIdGenerator correlationIdGenerator)
  {
    var correlationId = GetCorrelationId(context, correlationIdGenerator);

    AddCorrelationIdHeaderResponse(context, correlationId);

    await _next(context);
  }

  private static void AddCorrelationIdHeaderResponse(HttpContext context,
    StringValues correlationId)
  {
    context.Response.OnStarting(() =>
    {
      context.Response.Headers.Add(_correlationIdHeader, new[] { correlationId.ToString() });

      return Task.CompletedTask;
    });
  }

  private static StringValues GetCorrelationId(HttpContext context,
    ICorrelationIdGenerator correlationIdGenerator)
  {
    if (context.Request.Headers.TryGetValue(_correlationIdHeader, out var correlationId))
    {
      correlationIdGenerator.Set(correlationId!);
      return correlationId;
    }

    return correlationIdGenerator.Get();
  }
}

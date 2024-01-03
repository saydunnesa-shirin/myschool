using Serilog;
using Serilog.Events;

namespace Api.Infrastructure.Logging;

public static class LogHelper
{
    public static void EnrichFromRequest(IDiagnosticContext diagnosticContext, HttpContext httpContext)
    {
        var request = httpContext.Request;

        diagnosticContext.Set("Host", request.Host);
        diagnosticContext.Set("Protocol", request.Protocol);
        diagnosticContext.Set("Scheme", request.Scheme);

        if (request.QueryString.HasValue)
        {
            diagnosticContext.Set("QueryString", request.QueryString.Value);
        }

        diagnosticContext.Set("business_transaction_id", httpContext.Response.Headers["X-Request-Id"]);
        diagnosticContext.Set("ContentType", httpContext.Response.ContentType);

        // Retrieve the IEndpointFeature selected for the request
        var endpoint = httpContext.GetEndpoint();
        if (endpoint is object) // endpoint != null
        {
            diagnosticContext.Set("EndpointName", endpoint.DisplayName);
        }
    }

    public static LogEventLevel ExcludeHealthChecks(HttpContext ctx, double _, Exception? ex)
    {
        if (ex != null || ctx.Response.StatusCode > 499) // check if 500-level server error
        {
            return LogEventLevel.Error;
        }

        if (IsHealthCheckEndpoint(ctx)) // Not an error, check if it was a health check
        {
            return LogEventLevel.Verbose; // Was a health check, use Verbose
        }

        return LogEventLevel.Information;
    }

    private static bool IsHealthCheckEndpoint(HttpContext ctx)
    {
        var endpoint = ctx.GetEndpoint();
        if (endpoint is not null)
        {
            return string.Equals(
                endpoint.DisplayName,
                "Health checks",
                StringComparison.Ordinal);
        }

        // No endpoint, so not a health check endpoint
        return false;
    }
}
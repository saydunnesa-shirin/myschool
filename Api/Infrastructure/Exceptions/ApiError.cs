using System.Net;
using System.Text.Json.Serialization;

namespace Api.Infrastructure.Exceptions;

public class ApiError
{
    /// <summary> Standardised API error code </summary>
    [JsonPropertyName("code")]
    public int Code { get; set; }

    /// <summary> Error message </summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    /// <summary> Dev/test environment stack trace field </summary>
    [JsonPropertyName("cause")]
    public string? Cause { get; set; }
}

public enum ApiStatusCode
{
    NotFound = HttpStatusCode.NotFound,
    BadRequest = HttpStatusCode.BadRequest,
    NotImplemented = HttpStatusCode.NotImplemented,
    Unauthorized = HttpStatusCode.Unauthorized,
    InternalServerError = HttpStatusCode.InternalServerError
}

public static class ApiStatusCodeHelper
{
    public static ApiStatusCode FromException(Exception exception)
    {
        return exception switch
        {
            NotFoundException => ApiStatusCode.NotFound,
            BadHttpRequestException => ApiStatusCode.BadRequest,
            NotImplementedException => ApiStatusCode.NotImplemented,
            UnauthorizedAccessException => ApiStatusCode.Unauthorized,
            _ => ApiStatusCode.InternalServerError
        };
    }
}

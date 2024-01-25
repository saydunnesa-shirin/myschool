using Api.Infrastructure.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Features.Countries;

[ApiController]
[Route("[controller]")]
public class CountriesController : Controller
{
    private readonly ISender _sender;

    public CountriesController(ISender sender)
    {
        _sender = sender;
    }

    /// <remarks>
    ///   Create country
    /// </remarks>
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CreateCountry.Result))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable, Type = typeof(ApiError))]
    public async Task<ActionResult<CreateCountry.Result>> CreateAsync(
      CreateCountry.Command command,
      CancellationToken cancellationToken)
    {
        return Ok(await _sender.Send(command, cancellationToken));
    }

    /// <remarks>
    ///   Get list of employees based on query filters
    /// </remarks>
    [HttpPost("query")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<GetCountries.Result>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable, Type = typeof(ApiError))]
    public async Task<ActionResult<List<GetCountries.Result>>> GetListByQueryAsync(
      GetCountries.Query query,
      CancellationToken cancellationToken)
    {
        return Ok(await _sender.Send(query, cancellationToken));
    }
}

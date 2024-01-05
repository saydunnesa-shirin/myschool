using System.Net;
using Api.Infrastructure.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Api.Features.Employees.GetEmployee;

namespace Api.Features.Employees;

[ApiController]
[Route("[controller]")]
public class EmployeesController : ControllerBase
{
  private readonly ISender _sender;

  public EmployeesController(ISender sender)
  {
    _sender = sender;
  }

  /// <remarks>
  ///   Get product classes returns product classes for case owners country.
  /// </remarks>
  [HttpGet("single")]
  [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Result))]
  [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiError))]
  [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiError))]
  [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ApiError))]
  [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ApiError))]
  [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable, Type = typeof(ApiError))]
  public async Task<ActionResult<Result>> GetSingleEmployee(
    [FromQuery] Query query,
    CancellationToken cancellationToken)
  {
    return Ok(await _sender.Send(query, cancellationToken));
  }
}

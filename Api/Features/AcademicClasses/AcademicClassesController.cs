using Api.Infrastructure.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Features.AcademicClasses;

[ApiController]
[Route("[controller]")]
public class AcademicClassesController : Controller
{
    private readonly ISender _sender;

    public AcademicClassesController(ISender sender)
    {
        _sender = sender;
    }

    /// <remarks>
    ///   Create AcademicClass
    /// </remarks>
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AcademicClassResult))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable, Type = typeof(ApiError))]
    public async Task<ActionResult<AcademicClassResult>> CreateAsync(
      CreateAcademicClass.Command command,
      CancellationToken cancellationToken)
    {
        return Ok(await _sender.Send(command, cancellationToken));
    }

    /// <remarks>
    ///   Update AcademicClass
    /// </remarks>
    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AcademicClassResult))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable, Type = typeof(ApiError))]
    public async Task<ActionResult<AcademicClassResult>> UpdateAsync(
      UpdateAcademicClass.Command command,
      CancellationToken cancellationToken)
    {
        return Ok(await _sender.Send(command, cancellationToken));
    }

    /// <remarks>
    ///   Delete AcademicClass
    /// </remarks>
    [HttpDelete]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AcademicClassResult))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable, Type = typeof(ApiError))]
    public async Task<ActionResult<AcademicClassResult>> DeleteAsync(
      DeleteAcademicClass.Command command,
      CancellationToken cancellationToken)
    {
        return Ok(await _sender.Send(command, cancellationToken));
    }

    /// <remarks>
    ///   Get list of AcademicClasses based on query filters
    /// </remarks>
    [HttpPost("query")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<AcademicClassResult>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable, Type = typeof(ApiError))]
    public async Task<ActionResult<List<AcademicClassResult>>> GetListByQueryAsync(
      GetAcademicClasses.Query query,
      CancellationToken cancellationToken)
    {
        return Ok(await _sender.Send(query, cancellationToken));
    }

    /// <remarks>
    ///   Get single AcademicClass
    /// </remarks>
    [HttpGet("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AcademicClassResult))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable, Type = typeof(ApiError))]
    public async Task<ActionResult<AcademicClassResult>> GetAsync(
      string id,
      CancellationToken cancellationToken)
    {
        GetAcademicClass.Query query = new() { Id = Convert.ToInt32(id) };
        return Ok(await _sender.Send(query, cancellationToken));
    }
}

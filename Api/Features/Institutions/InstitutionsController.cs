using Api.Features.Employees;
using Api.Infrastructure.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Features.Institutions;

[ApiController]
[Route("[controller]")]
public class InstitutionsController : Controller
{
    private readonly ISender _sender;

    public InstitutionsController(ISender sender)
    {
        _sender = sender;
    }

    /// <remarks>
    ///   Create institution
    /// </remarks>
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CreateInstitution.Result))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable, Type = typeof(ApiError))]
    public async Task<ActionResult<CreateInstitution.Result>> CreateAsync(
      CreateInstitution.Command command,
      CancellationToken cancellationToken)
    {
        return Ok(await _sender.Send(command, cancellationToken));
    }

    /// <remarks>
    ///   Update institution
    /// </remarks>
    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UpdateInstitution.Result))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable, Type = typeof(ApiError))]
    public async Task<ActionResult<UpdateInstitution.Result>> UpdateAsync(
      UpdateInstitution.Command command,
      CancellationToken cancellationToken)
    {
        return Ok(await _sender.Send(command, cancellationToken));
    }

    /// <remarks>
    ///   Delete institution
    /// </remarks>
    [HttpDelete]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GetInstitution.Result))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable, Type = typeof(ApiError))]
    public async Task<ActionResult<DeleteInstitution>> DeleteAsync(
      DeleteInstitution.Command command,
      CancellationToken cancellationToken)
    {
        return Ok(await _sender.Send(command, cancellationToken));
    }

    /// <remarks>
    ///   Get list of institutions based on query filters
    /// </remarks>
    [HttpPost("query")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<GetInstitutions.Result>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable, Type = typeof(ApiError))]
    public async Task<ActionResult<List<GetInstitutions.Result>>> GetListByQueryAsync(
      GetInstitutions.Query query,
      CancellationToken cancellationToken)
    {
        return Ok(await _sender.Send(query, cancellationToken));
    }

    /// <remarks>
    ///   Get single institution
    /// </remarks>
    [HttpGet("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GetInstitution.Result))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable, Type = typeof(ApiError))]
    public async Task<ActionResult<GetInstitution.Result>> GetAsync(
      string id,
      CancellationToken cancellationToken)
    {
        GetInstitution.Query query = new() { Id = Convert.ToInt32(id) };
        return Ok(await _sender.Send(query, cancellationToken));
    }
}

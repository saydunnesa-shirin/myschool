using System.Net;
using Api.Features.Countries;
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
    ///   Get single employee
    /// </remarks>
    [HttpGet("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(EmployeeResult))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable, Type = typeof(ApiError))]
    public async Task<ActionResult<EmployeeResult>> GetAsync(
      string id,
      CancellationToken cancellationToken)
    {
        Query query = new Query { Id = Convert.ToInt32(id) };
        return Ok(await _sender.Send(query, cancellationToken));
    }

    /// <remarks>
    ///   Get list of employees based on query filters
    /// </remarks>
    [HttpPost("query")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<EmployeeResult>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable, Type = typeof(ApiError))]
    public async Task<ActionResult<List<EmployeeResult>>> GetListByQueryAsync(
      GetEmployees.Query query,
      CancellationToken cancellationToken)
    {
        return Ok(await _sender.Send(query, cancellationToken));
    }

    /// <remarks>
    ///   Create employee
    /// </remarks>
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(EmployeeResult))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable, Type = typeof(ApiError))]
    public async Task<ActionResult<EmployeeResult>> CreateAsync(
      CreateEmployee.Command command,
      CancellationToken cancellationToken)
    {
        return Ok(await _sender.Send(command, cancellationToken));
    }

    /// <remarks>
    ///   Update employee
    /// </remarks>
    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(EmployeeResult))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable, Type = typeof(ApiError))]
    public async Task<ActionResult<EmployeeResult>> UpdateAsync(
      UpdateEmployee.Command command,
      CancellationToken cancellationToken)
    {
        return Ok(await _sender.Send(command, cancellationToken));
    }

    /// <remarks>
    ///   Delete employee
    /// </remarks>
    [HttpDelete]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(EmployeeResult))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable, Type = typeof(ApiError))]
    public async Task<ActionResult<EmployeeResult>> DeleteAsync(
      DeleteEmployee.Command command,
      CancellationToken cancellationToken)
    {
        return Ok(await _sender.Send(command, cancellationToken));
    }
}

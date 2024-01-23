using Api.Common;
using Api.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Features.Commons;

[ApiController]
[Route("[controller]")]
public class TypesController : Controller
{
    /// <remarks>
    ///   Get list of designations
    /// </remarks>
    [HttpGet("designations")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<TypeViewModel>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable, Type = typeof(ApiError))]
    public ActionResult<List<TypeViewModel>> GetDesignationsAsync()
    {
        var list = Enum.GetValues(typeof(Designation))
               .Cast<Designation>()
               .Select(t => new TypeViewModel
               {
                   Id = ((int)t),
                   Name = t.ToString()
               })
               .ToList();

        return Ok(list);
    }

    /// <remarks>
    ///   Get list of employee types
    /// </remarks>
    [HttpGet("employeetypes")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<TypeViewModel>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable, Type = typeof(ApiError))]
    public ActionResult<List<TypeViewModel>> GetEmployeeTypesAsync()
    {
        var list = Enum.GetValues(typeof(EmployeeType))
               .Cast<EmployeeType>()
               .Select(t => new TypeViewModel
               {
                   Id = ((int)t),
                   Name = t.ToString()
               })
               .ToList();

        return Ok(list);
    }

    /// <remarks>
    ///   Get list of genders
    /// </remarks>
    [HttpGet("genders")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<TypeViewModel>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ApiError))]
    [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable, Type = typeof(ApiError))]
    public ActionResult<List<TypeViewModel>> GetGendersAsync()
    {
        var list = Enum.GetValues(typeof(Gender))
               .Cast<Gender>()
               .Select(t => new TypeViewModel
               {
                   Id = ((int)t),
                   Name = t.ToString()
               })
               .ToList();

        return Ok(list);
    }
}

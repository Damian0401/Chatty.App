using System.Net;
using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    protected IMediator Mediator { get; }

    public BaseController(IServiceProvider serviceProvider)
    {
        Mediator = serviceProvider.GetService<IMediator>()!;
    }

    protected IActionResult SendResponse(ResponseForController response)
    {
        return response.StatusCode switch
        {
            HttpStatusCode.OK => NoContent(),
            HttpStatusCode.Unauthorized => Unauthorized(),
            HttpStatusCode.Forbidden => Forbid(),
            HttpStatusCode.NotFound => NotFound(),
            _ => BadRequest(new { Errors = response.Errors })
        };
    }

    protected IActionResult SendResponse<T>(ResponseForController<T> response)
    {
        return response.StatusCode switch
        {
            HttpStatusCode.OK => Ok(response.ResponseContent),
            HttpStatusCode.Unauthorized => Unauthorized(),
            HttpStatusCode.Forbidden => Forbid(),
            HttpStatusCode.NotFound => NotFound(),
            _ => BadRequest(new { Errors = new { response.Errors } })
        };
    }
}
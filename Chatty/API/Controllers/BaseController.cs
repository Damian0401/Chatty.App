using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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

    protected IActionResult SendResponse(Response response)
    {
        return response.StatusCode switch
        {
            HttpStatusCode.OK => Ok(),
            HttpStatusCode.Unauthorized => Unauthorized(),
            HttpStatusCode.Forbidden => Forbid(),
            HttpStatusCode.NotFound => NotFound(),
            _ => BadRequest(new { Errors = response.Errors })
        };
    }

    protected IActionResult SendResponse<T>(Response<T> response)
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
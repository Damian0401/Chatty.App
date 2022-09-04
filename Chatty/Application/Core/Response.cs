using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Application.Core;
public class Response
{
    public HttpStatusCode StatusCode { get; set; }
    public List<string>? Errors { get; set; }

    public Response(HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
    }

    public Response(HttpStatusCode statusCode, List<string> errors)
    {
        StatusCode = statusCode;
        Errors = errors;
    }
}

public class Response<T>
{
    public HttpStatusCode StatusCode { get; set; }
    public List<string>? Errors { get; set; }
    public T? ResponseContent { get; set; }

    public Response(HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
    }

    public Response(HttpStatusCode statusCode, List<string> errors)
    {
        StatusCode = statusCode;
        Errors = errors;
    }

    public Response(HttpStatusCode statusCode, T responseContent)
    {
        StatusCode = statusCode;
        ResponseContent = responseContent;
    }
}
using System.Net;

namespace Application.Core;
public class ResponseForController
{
    public HttpStatusCode StatusCode { get; set; }
    public List<string>? Errors { get; set; }

    public ResponseForController(HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
    }

    public ResponseForController(HttpStatusCode statusCode, List<string> errors)
    {
        StatusCode = statusCode;
        Errors = errors;
    }
}

public class ResponseForController<T>
{
    public HttpStatusCode StatusCode { get; set; }
    public List<string>? Errors { get; set; }
    public T? ResponseContent { get; set; }

    public ResponseForController(HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
    }

    public ResponseForController(HttpStatusCode statusCode, List<string> errors)
    {
        StatusCode = statusCode;
        Errors = errors;
    }

    public ResponseForController(HttpStatusCode statusCode, T responseContent)
    {
        StatusCode = statusCode;
        ResponseContent = responseContent;
    }
}
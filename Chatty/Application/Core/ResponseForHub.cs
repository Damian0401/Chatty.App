using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core;

public class ResponseForHub<T>
{
    public bool IsSuccess { get; set; }
    public T? ResponseContent { get; set; }

    public List<string>? Errors { get; set; }

    public static ResponseForHub<T> Success(T responseContent)
        => new ResponseForHub<T> { IsSuccess = true, ResponseContent = responseContent };

    public static ResponseForHub<T> Failure(List<string> errors)
        => new ResponseForHub<T> { IsSuccess = false, Errors = errors };
}

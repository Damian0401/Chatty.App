using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Middleware;

public class WebSocketsMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var request = context.Request;

        if (request.Path.StartsWithSegments("/hubs/chat", StringComparison.OrdinalIgnoreCase) &&
            request.Query.TryGetValue("access_token", out var accessToken))
        {
            request.Headers.Add("Authorization", $"Bearer {accessToken}");
        }

        await next(context);
    }
}

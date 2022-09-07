using Application.Core;
using Microsoft.AspNetCore.SignalR;

namespace API.Hubs;

public class BaseHub : Hub
{
    protected async Task HandleErrors<T>(ResponseForHub<T> response)
    {
        await Clients.Caller.SendAsync("HandleErrors", new { Errors = response.Errors });
    }
}
using Application.Core;
using Microsoft.AspNetCore.SignalR;

namespace API.Hubs;

public class BaseHub : Hub
{
    protected async Task HandleErrors(List<string>? errors)
    {
        if (errors is not null)
            await Clients.Caller.SendAsync("HandleErrors", new { Errors = errors });
    }
}
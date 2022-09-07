using System.Net;
using Application.Core;
using Application.Dtos.Room;
using Application.Rooms;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace API.Hubs;

[Authorize]
public class ChatHub : BaseHub
{
    private readonly IMediator _mediator;

    public ChatHub(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task CreateRoom(CreateRoomRequestDto dto)
    {
        var response = await _mediator.Send(new CreateRoom.Command { Dto = dto });

        if (!response.IsSuccess)
        {
            await HandleErrors(response);
            return;
        }

        await Groups.AddToGroupAsync(Context.ConnectionId, 
            response.ResponseContent!.Id.ToString());

        await Clients.Caller
            .SendAsync("AddRoom", response.ResponseContent);
    }

    public async Task JoinRoom(JoinRoomRequestDto dto)
    {
        var response = await _mediator.Send(new JoinRoom.Command { Dto = dto });

        if (!response.IsSuccess)
        {
            await HandleErrors(response);
            return;
        }

        await Clients.Group(response.ResponseContent!.CallerResponse.Id.ToString())
            .SendAsync("AddUser", response.ResponseContent.ClientsResponse);

        await Groups.AddToGroupAsync(Context.ConnectionId, 
            response.ResponseContent.CallerResponse.Id.ToString());

        await Clients.Caller
            .SendAsync("AddRoom", response.ResponseContent.CallerResponse);
    }
}
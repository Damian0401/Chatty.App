using System.Net;
using Application.Core;
using Application.Dtos.Message;
using Application.Dtos.Room;
using Application.Messages;
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

    public async override Task OnConnectedAsync()
    {
        var response = await _mediator.Send(new ConnectToChat.Query());

        if (!response.IsSuccess)
        {
            await HandleErrors(response.Errors);
            return;
        }

        foreach (var room in response.ResponseContent.Rooms)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, room.Id.ToString());
        }

        await Clients.Caller
            .SendAsync("ConnectToChat", response.ResponseContent);
    }

    public async Task CreateRoom(CreateRoomRequestDto dto)
    {
        var response = await _mediator.Send(new CreateRoom.Command { Dto = dto });

        if (!response.IsSuccess)
        {
            await HandleErrors(response.Errors);
            return;
        }

        await Groups.AddToGroupAsync(Context.ConnectionId,
            response.ResponseContent.Room.Id.ToString());

        await Clients.Caller
            .SendAsync("AddRoom", response.ResponseContent);
    }

    public async Task JoinRoom(JoinRoomRequestDto dto)
    {
        var response = await _mediator.Send(new JoinRoom.Command { Dto = dto });

        if (!response.IsSuccess)
        {
            await HandleErrors(response.Errors);
            return;
        }

        await Clients.Group(response.ResponseContent.CallerResponse.Room.Id.ToString())
            .SendAsync("AddUser", response.ResponseContent.ClientsResponse);

        await Groups.AddToGroupAsync(Context.ConnectionId,
            response.ResponseContent.CallerResponse.Room.Id.ToString());

        await Clients.Caller
            .SendAsync("AddRoom", response.ResponseContent.CallerResponse);
    }

    public async Task SendMessage(SendMessageRequestDto dto)
    {
        var response = await _mediator.Send(new SendMessage.Command { Dto = dto });

        if (!response.IsSuccess)
        {
            await HandleErrors(response.Errors);
            return;
        }

        await Clients.Group(dto.RoomId.ToString())
            .SendAsync("RecieveMessage", response.ResponseContent);
    }

    public async Task DeleteMessage(DeleteMessageRequestDto dto)
    {
        var response = await _mediator.Send(new DeleteMessage.Command { Dto = dto });

        if (!response.IsSuccess)
        {
            await HandleErrors(response.Errors);
            return;
        }

        await Clients.Group(response.ResponseContent.Message.RoomId.ToString())
            .SendAsync("RecieveMessage", response.ResponseContent);
    }

    public async Task RoomDetails(GetRoomDetailsRequestDto dto)
    {
        var response = await _mediator.Send(new GetRoomDetails.Query{ Dto = dto });

        if (!response.IsSuccess)
        {
            await HandleErrors(response.Errors);
            return;
        }

        await Clients.Caller
            .SendAsync("GetRoomDetails", response.ResponseContent);
    }
}
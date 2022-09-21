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
            .SendAsync("ConnectToChat", response.ResponseContent.Rooms);
    }

    public async Task CreateRoom(string roomName)
    {
        var response = await _mediator.Send(new CreateRoom.Command { RoomName = roomName });

        if (!response.IsSuccess)
        {
            await HandleErrors(response.Errors);
            return;
        }

        await Groups.AddToGroupAsync(Context.ConnectionId,
            response.ResponseContent.Id.ToString());

        await Clients.Caller
            .SendAsync("AddRoom", response.ResponseContent);
    }

    public async Task JoinRoom(Guid roomId)
    {
        var response = await _mediator.Send(new JoinRoom.Command { RoomId = roomId });

        if (!response.IsSuccess)
        {
            await HandleErrors(response.Errors);
            return;
        }

        await Clients.Group(response.ResponseContent.CallerResponse.Id.ToString())
            .SendAsync("AddToRoom", response.ResponseContent.ClientsResponse);

        await Groups.AddToGroupAsync(Context.ConnectionId,
            response.ResponseContent.CallerResponse.Id.ToString());

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

    public async Task DeleteMessage(Guid messageId)
    {
        var response = await _mediator.Send(new DeleteMessage.Command { MessageId = messageId });

        if (!response.IsSuccess)
        {
            await HandleErrors(response.Errors);
            return;
        }

        await Clients.Group(response.ResponseContent.RoomId.ToString())
            .SendAsync("RecieveMessage", response.ResponseContent);
    }

    public async Task DeleteRoom(Guid roomId)
    {
        var response = await _mediator.Send(new DeleteRoom.Command { RoomId = roomId });

        if (!response.IsSuccess)
        {
            await HandleErrors(response.Errors);
            return;
        }

        await Clients.Group(roomId.ToString())
            .SendAsync("HandleDeleteRoom", roomId);
    }

    public async Task RoomDetails(Guid roomId)
    {
        var response = await _mediator.Send(new GetRoomDetails.Query { RoomId = roomId });

        if (!response.IsSuccess)
        {
            await HandleErrors(response.Errors);
            return;
        }

        await Clients.Caller
            .SendAsync("GetRoomDetails", response.ResponseContent);
    }

    public async Task ChangeDisplayName(ChangeDisplayNameRequestDto dto)
    {
        var response = await _mediator.Send(new ChangeDisplayName.Command { Dto = dto });

        if (!response.IsSuccess)
        {
            await HandleErrors(response.Errors);
            return;
        }

        await Clients.Group(response.ResponseContent.Id.ToString())
            .SendAsync("AddToRoom", response.ResponseContent);
    }

    public async Task LeaveGroup(string groupName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }
}
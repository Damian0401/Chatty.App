using Application.Dtos.Account;
using Application.Dtos.Message;
using Domain.Models;

namespace Application.Dtos.Room;

public class ConnectToChatResponseDto
{
    public List<RoomForConnectToChatResponseDto> Rooms { get; set; } = default!;
}

public class RoomForConnectToChatResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
}
using Application.Dtos.Message;
using Domain.Models;

namespace Application.Dtos.Room;

public class ConnectToChatResponseDto
{
    public List<RoomDto> Rooms { get; set; } = default!;
}
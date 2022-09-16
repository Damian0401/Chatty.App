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
    public List<MessageForConnectToChatResponseDto> Messages { get; set; } = default!;
}

public class MessageForConnectToChatResponseDto
{
    public Guid Id { get; set; }
    public string Body { get; set; } = default!;
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? AuthorId { get; set; }
    public Guid RoomId { get; set; }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Account;
using Application.Dtos.Message;

namespace Application.Dtos.Room;

public class JoinRoomResponseDto
{
    public CallerResponseForJoinRoomResponseDto CallerResponse { get; set; } = default!;
    public ClientsResponseForJoinRoomResponseDto ClientsResponse { get; set; } = default!;
}

public class CallerResponseForJoinRoomResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public List<MessageForJoinRoomResponseDto> Messages { get; set; } = default!;
    public List<UserForJoinRoomResponseDto> Users { get; set; } = default!;
}

public class ClientsResponseForJoinRoomResponseDto
{
    public Guid Id { get; set; }
    public UserForJoinRoomResponseDto User { get; set; } = default!;
    public MessageForJoinRoomResponseDto Message { get; set; } = default!;
}

public class MessageForJoinRoomResponseDto
{
    public Guid Id { get; set; }
    public string Body { get; set; } = default!;
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? AuthorId { get; set; }
    public Guid RoomId { get; set; }
}

public class UserForJoinRoomResponseDto
{
    public string Id { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
    public bool IsAdministrator { get; set; }
}
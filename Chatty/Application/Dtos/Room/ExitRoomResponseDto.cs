using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos.Room;

public class ExitRoomResponseDto
{
    public bool IsRoomDeleted { get; set; }
    public ClientsResponseForExitRoomResponseDto ClientsResponse { get; set; } = default!;
}

public class ClientsResponseForExitRoomResponseDto
{
    public string UserId { get; set; } = default!;
    public Guid RoomId { get; set; }
    public MessageForExitRoomResponseDto Message { get; set; } = default!;
}

public class MessageForExitRoomResponseDto
{
    public Guid Id { get; set; }
    public string Body { get; set; } = default!;
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? AuthorId { get; set; }
    public Guid RoomId { get; set; }
}
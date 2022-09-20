using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos.Room;

public class ChangeDisplayNameResponseDto
{
    public Guid Id { get; set; }
    public UserForJoinRoomResponseDto User { get; set; } = default!;
    public MessageForJoinRoomResponseDto Message { get; set; } = default!;
}

public class MessageForChangeDisplayNameResponseDto
{
    public Guid Id { get; set; }
    public string Body { get; set; } = default!;
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? AuthorId { get; set; }
    public Guid RoomId { get; set; }
}

public class UserForChangeDisplayNameResponseDto
{
    public string Id { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
    public bool IsAdministrator { get; set; }
}
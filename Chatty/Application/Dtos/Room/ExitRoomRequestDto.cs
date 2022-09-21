using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos.Room;

public class ExitRoomRequestDto
{
    public string UserId { get; set; } = default!;
    public Guid RoomId { get; set; }
}
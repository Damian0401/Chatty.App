using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos.Room;

public class ChangeDisplayNameRequestDto
{
    public string UserId { get; set; } = default!;
    public Guid RoomId { get; set; }
    public string DisplayName { get; set; } = default!;
}
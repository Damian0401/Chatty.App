using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos.Room;

public class CreateRoomRequestDto
{
    public string Name { get; set; } = default!;
}
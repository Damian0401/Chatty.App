using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Account;
using Application.Dtos.Message;

namespace Application.Dtos.Room;

public class JoinRoomResponseDto
{
    public RoomDto CallerResponse { get; set; } = default!;
    public AddToRoomDto ClientsResponse { get; set; } = default!;
}
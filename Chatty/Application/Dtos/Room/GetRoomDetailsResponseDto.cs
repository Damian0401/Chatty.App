using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Account;
using Application.Dtos.Message;

namespace Application.Dtos.Room;

public class GetRoomDetailsResponseDto
{
    public RoomDto Room { get; set; } = default!;
}
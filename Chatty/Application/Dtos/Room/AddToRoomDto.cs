using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Account;
using Application.Dtos.Message;

namespace Application.Dtos.Room;

public class AddToRoomDto
{
    public ApplicationUserDto User { get; set; } = default!;
    public MessageDto Message { get; set; } = default!;
}
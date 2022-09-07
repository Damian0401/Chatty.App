using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Account;
using Application.Dtos.Message;

namespace Application.Dtos.Room;

public class CreateRoomResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public List<MessageDto> Messages { get; set; } = default!;
    public List<ApplicationUserDto> Users { get; set; } = default!;
}
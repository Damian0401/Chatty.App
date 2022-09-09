using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos.Message;

public class SendMessageRequestDto
{
    public string Body { get; set; } = default!;
    public Guid RoomId { get; set; }
}
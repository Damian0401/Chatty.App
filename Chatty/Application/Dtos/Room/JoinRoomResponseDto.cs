using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Account;
using Application.Dtos.Message;

namespace Application.Dtos.Room;

public class JoinRoomResponseDto
{
    public JoinRoomResponseForCallerDto CallerResponse { get; set; } = default!;
    public JoinRoomResponseForClientsDto ClientsResponse { get; set; } = default!;
}

public class JoinRoomResponseForClientsDto
{
    public ApplicationUserDto User { get; set; } = default!;
    public MessageDto Message { get; set; } = default!;
}

public class JoinRoomResponseForCallerDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public List<MessageDto> Messages { get; set; } = default!;
    public List<ApplicationUserDto> Users { get; set; } = default!;
}